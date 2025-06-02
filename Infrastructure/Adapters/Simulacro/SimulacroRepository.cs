
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
            _collection = context._dbs.GetCollection<SimulacroEntity>("Simulacrum");
            _collectionQuestion = context._dbs.GetCollection<QuestionEntity>("Question");

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
            var filterCompetence = Builders<QuestionEntity>.Filter.Eq(q => q.IdCompetence, idCompetence);
            var allQuestions = await _collectionQuestion.Find(filterCompetence).ToListAsync();

            if (allQuestions == null || allQuestions.Count == 0)
            {
                return new List<string>();
            }
            var questionGroups = allQuestions
                .GroupBy(q => q.IdInfoQuestion)
                .Select(group => new
                {
                    Questions = group.OrderBy(q => q.DateUpdate).ThenBy(q => q.Id).ToList(),
                    GroupId = group.Key
                })
                .ToList();

            var random = new Random();
            var shuffledGroups = questionGroups.OrderBy(g => random.Next()).ToList();

            var selectedQuestions = new List<string>();

            foreach (var group in shuffledGroups)
            {
                foreach (var question in group.Questions)
                {
                    if (selectedQuestions.Count < numeroPreguntasByCompetence)
                    {
                        selectedQuestions.Add(question.Id);
                    }
                    else
                    {
                        break;
                    }
                }

                if (selectedQuestions.Count >= numeroPreguntasByCompetence)
                {
                    break;
                }
            }
            return selectedQuestions.Take(numeroPreguntasByCompetence).ToList();
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