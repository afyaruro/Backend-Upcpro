
using Domain.Base.ResponseEntity;
using Domain.Entity.Question;
using Domain.Entity.Simulacros;
using Domain.Port.Simulacro;
using Infrastructure.Context.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Adapters.Simulacro
{
    public class SimulacroRepository : ISimulacroRepository
    {
        private readonly IMongoCollection<SimulacroEntity> _collection;
        private readonly IMongoCollection<QuestionEntity> _collectionQuestion;


        public SimulacroRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<SimulacroEntity>("Simlacro");
            _collectionQuestion = context._dbs.GetCollection<QuestionEntity>("Question");

        }

        public async Task<SimulacroEntity> CrearAsync(SimulacroEntity simulacro)
        {
            await _collection.InsertOneAsync(simulacro);
            return simulacro;
        }

        public async Task<bool> ActualizarAsync(SimulacroEntity simulacro)
        {

            var updateResult = await _collection.ReplaceOneAsync(
                filter: doc => doc.Id == simulacro.Id,
                replacement: simulacro);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }

        public async Task<bool> EliminarAsync(string id)
        {

            var deleteResult = await _collection.DeleteOneAsync(doc => doc.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }

        public async Task<ResponseEntity<SimulacroEntity>> GetSimulacrosAsync(DateTime fechaActual)
        {
            var filter = Builders<SimulacroEntity>.Filter.Gte(s => s.FechaLimite, fechaActual);
            var simulacros = await _collection.Find(filter).ToListAsync();

            if (simulacros == null || !simulacros.Any())
            {
                return new ResponseEntity<SimulacroEntity>("No se encontraron simulacros disponibles para la fecha actual", false);
            }

            return new ResponseEntity<SimulacroEntity>($"Se encontraron {simulacros.Count} simulacros disponibles", simulacros)
            {
                totalPages = 1,
                totalRecords = simulacros.Count
            };
        }

        public async Task<SimulacroEntity?> GetById(string id)
        {
            return await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> ExistById(string id)
        {
            return await _collection.Find(c => c.Id == id).AnyAsync();
        }





        public async Task<List<string>> GenerateQuestionCompetence(int numeroPreguntasByCompetence, string idCompetence)
        {
            var filterQuestions = Builders<QuestionEntity>.Filter.Eq(q => q.IdCompetence, idCompetence);
            long totalQuestions = await _collectionQuestion.CountDocumentsAsync(filterQuestions);

            if (totalQuestions == 0)
            {
                return [];
            }

            var pipeline = new BsonDocument[]
            {
        new BsonDocument("$match", new BsonDocument("idCompetence", idCompetence)),
        new BsonDocument("$sample", new BsonDocument("size", numeroPreguntasByCompetence)),
        new BsonDocument("$project", new BsonDocument("_id", 1)) // Solo proyectar el ID
            };

            var questionIds = new List<string>();
            var cursor = await _collectionQuestion.AggregateAsync<BsonDocument>(pipeline);

            await cursor.ForEachAsync(doc =>
            {
                if (doc.Contains("_id"))
                {
                    questionIds.Add(doc["_id"].ToString()!);
                }
            });

            return questionIds;
        }

        public bool ExistNumQuestion(int numQuestion, string idCompetence)
        {
            var filter = Builders<QuestionEntity>.Filter.Eq(q => q.IdCompetence, idCompetence);
            var count = _collectionQuestion.CountDocuments(filter);

            if (count >= numQuestion)
            {
                return true;
            }
            return false;

        }

        
    }
}