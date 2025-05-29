
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


        // public async Task<List<string>> GenerateQuestionCompetence(int numeroPreguntasByCompetence, string idCompetence)
        // {
        //     // 1. Obtener todas las preguntas de la competencia
        //     var filterCompetence = Builders<QuestionEntity>.Filter.Eq(q => q.IdCompetence, idCompetence);
        //     var allQuestions = await _collectionQuestion.Find(filterCompetence).ToListAsync();

        //     // Si no hay preguntas, retornamos una lista vacía
        //     if (allQuestions == null || allQuestions.Count == 0)
        //     {
        //         return new List<string>();
        //     }

        //     // 2. Conjunto para llevar el control de las preguntas ya seleccionadas (suponiendo que 'Id' identifica a cada pregunta)
        //     HashSet<string> selectedQuestionIds = new HashSet<string>();

        //     // Inicializamos el random afuera del ciclo para no reinicializar la semilla en cada iteración
        //     Random rnd = new Random();

        //     // 3. Iterar mientras no se complete el número requerido y existan preguntas por seleccionar
        //     while (selectedQuestionIds.Count < numeroPreguntasByCompetence &&
        //            allQuestions.Any(q => !selectedQuestionIds.Contains(q.Id)))
        //     {
        //         // Obtener las preguntas que aún no han sido seleccionadas
        //         var availableQuestions = allQuestions.Where(q => !selectedQuestionIds.Contains(q.Id)).ToList();

        //         // Seleccionar una pregunta aleatoria de las disponibles
        //         var randomQuestion = availableQuestions[rnd.Next(availableQuestions.Count)];

        //         // Filtrar todas las preguntas relacionadas por el mismo IdInfoQuestion
        //         var relatedQuestions = allQuestions
        //                                .Where(q => q.IdInfoQuestion == randomQuestion.IdInfoQuestion &&
        //                                            !selectedQuestionIds.Contains(q.Id))
        //                                .ToList();

        //         // Agregar las preguntas relacionadas a la lista, respetando el límite
        //         foreach (var question in relatedQuestions)
        //         {
        //             if (selectedQuestionIds.Count < numeroPreguntasByCompetence)
        //             {
        //                 selectedQuestionIds.Add(question.Id);
        //             }
        //             else
        //             {
        //                 break;
        //             }
        //         }
        //     }

        //     // Si se requiriera aleatorizar el resultado final antes de retornarlo, se podría hacer:
        //     var result = selectedQuestionIds
        //                     .OrderBy(id => rnd.Next())
        //                     .Take(numeroPreguntasByCompetence)
        //                     .ToList();

        //     return result;
        // }


        public async Task<List<string>> GenerateQuestionCompetence(int numeroPreguntasByCompetence, string idCompetence)
        {
            // 1. Obtener todas las preguntas de la competencia
            var filterCompetence = Builders<QuestionEntity>.Filter.Eq(q => q.IdCompetence, idCompetence);
            var allQuestions = await _collectionQuestion.Find(filterCompetence).ToListAsync();

            // Si no hay preguntas, retornamos una lista vacía
            if (allQuestions == null || allQuestions.Count == 0)
            {
                return new List<string>();
            }

            // 2. Agrupar preguntas por IdInfoQuestion y ordenar dentro de cada grupo
            var questionGroups = allQuestions
                .GroupBy(q => q.IdInfoQuestion)
                .Select(group => new
                {
                    Questions = group.OrderBy(q => q.DateUpdate).ThenBy(q => q.Id).ToList(),
                    GroupId = group.Key
                })
                .ToList();

            // 3. Aleatorizar el orden de los grupos
            var random = new Random();
            var shuffledGroups = questionGroups.OrderBy(g => random.Next()).ToList();

            // 4. Seleccionar preguntas manteniendo el orden dentro de cada grupo
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

            // 5. Si quieres que el resultado final esté completamente aleatorizado
            // return selectedQuestions.OrderBy(id => random.Next()).ToList();

            // Retornar manteniendo el orden de grupos (aleatorio) pero orden interno preservado
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