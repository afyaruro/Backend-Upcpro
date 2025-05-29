
using Domain.Entity;
using Domain.Entity.Simulacros;
using Domain.Port.SimulacrumResult;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.SimulacrumResult
{
    public class SimulacrumResultRepository : ISimulacrumResultRepository
    {

        private readonly IMongoCollection<SimulacrumResultEntity> _collection;
        private readonly IMongoCollection<UserEntity> _collectionUser;



        public SimulacrumResultRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<SimulacrumResultEntity>("SimulacrumResult");
            _collectionUser = context._dbs.GetCollection<UserEntity>("User");


        }

        public async Task<bool> CrearAsync(SimulacrumResultEntity simulacro)
        {
            await _collection.InsertOneAsync(simulacro);
            return true;
        }

        public async Task<bool> ExistByUser(string idUser, string idSimulacro)
        {
            var filter = Builders<SimulacrumResultEntity>.Filter.Eq(c => c.IdEstudiante, idUser) &
                         Builders<SimulacrumResultEntity>.Filter.Eq(c => c.IdSimulacro, idSimulacro);
            return await _collection.Find(filter).AnyAsync();
        }

        public async Task<List<SimulacrumResultEntity>> GetAll(string idUser)
        {
            var filter = Builders<SimulacrumResultEntity>.Filter.Eq(c => c.IdEstudiante, idUser);
            return await _collection.Find(filter).ToListAsync();
        }


        


        


    }
}