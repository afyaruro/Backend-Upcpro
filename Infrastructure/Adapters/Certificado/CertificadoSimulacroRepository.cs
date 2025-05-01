
using Domain.Entity;
using Domain.Entity.RankingResponseEntity;
using Domain.Entity.Simulacros;
using Domain.Port.Certificado;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.Certificado
{
    public class CertificadoSimulacroRepository : ICertificadoSimulacroRepository
    {

        private readonly IMongoCollection<SimulacroResultEntity> _collection;
        private readonly IMongoCollection<UserEntity> _collectionUser;



        public CertificadoSimulacroRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<SimulacroResultEntity>("ResultSimulacrum");
            _collectionUser = context._dbs.GetCollection<UserEntity>("User");


        }

        public async Task<bool> CrearAsync(SimulacroResultEntity simulacro)
        {
            await _collection.InsertOneAsync(simulacro);
            return true;
        }

        public async Task<bool> ExistByUser(string idUser, string idSimulacro)
        {
            var filter = Builders<SimulacroResultEntity>.Filter.Eq(c => c.IdEstudiante, idUser) &
                         Builders<SimulacroResultEntity>.Filter.Eq(c => c.IdSimulacro, idSimulacro);
            return await _collection.Find(filter).AnyAsync();
        }

        public async Task<List<SimulacroResultEntity>> GetAll(string idUser)
        {
            var filter = Builders<SimulacroResultEntity>.Filter.Eq(c => c.IdEstudiante, idUser);
            return await _collection.Find(filter).ToListAsync();
        }


        public async Task<(RankingResponseEntity<SimulacroResultEntity>, List<UserEntity>)> GetRankingByScore(string userId, string idSimulacro)
        {
            var simulacroFilter = Builders<SimulacroResultEntity>.Filter.Eq(s => s.IdSimulacro, idSimulacro) &
                                  Builders<SimulacroResultEntity>.Filter.Eq(s => s.TypeResult, "default");

            var topResults = await _collection.Find(simulacroFilter)
                .SortByDescending(u => u.Puntaje)
                .Limit(3)
                .ToListAsync();

            int userPosition = 0;
            var allResults = await _collection.Find(simulacroFilter)
                .SortByDescending(u => u.Puntaje)
                .ToListAsync();

            userPosition = allResults.FindIndex(r => r.IdEstudiante == userId) + 1;

            if (userPosition == 0)
            {
                userPosition = -1;
            }

            var userFilter = Builders<SimulacroResultEntity>.Filter.Eq(s => s.IdEstudiante, userId) &
                             Builders<SimulacroResultEntity>.Filter.Eq(s => s.IdSimulacro, idSimulacro) &
                             Builders<SimulacroResultEntity>.Filter.Eq(s => s.TypeResult, "default");

            var currentUserResult = await _collection.Find(userFilter).FirstOrDefaultAsync();

            List<UserEntity> users = [];
            foreach (var result in topResults)
            {
                var user = await _collectionUser.Find(u => u.Id == result.IdEstudiante).FirstOrDefaultAsync();

                users.Add(user);

            }



            return (new RankingResponseEntity<SimulacroResultEntity>
            {
                Top = topResults,
                CurrentUserPosition = userPosition,
            }, users);
        }


    }
}