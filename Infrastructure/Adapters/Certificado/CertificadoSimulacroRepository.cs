
using Domain.Entity.Simulacros;
using Domain.Port.Certificado;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.Certificado
{
    public class CertificadoSimulacroRepository : ICertificadoSimulacroRepository
    {

        private readonly IMongoCollection<SimulacroResultEntity> _collection;


        public CertificadoSimulacroRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<SimulacroResultEntity>("Certificado");

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
    }
}