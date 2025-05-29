
using Domain.Base.ResponseEntity;
using Domain.Entity.Question;
using Domain.Port;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.InfoQuestion
{
    public class InfoQuestionRepository : IQuestionRepository<InfoQuestionEntity>
    {
        private readonly IMongoCollection<InfoQuestionEntity> _collection;

        public InfoQuestionRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<InfoQuestionEntity>("Info_Question");

        }

        public async Task<bool> ExistById(string id)
        {
            return await _collection.Find(c => c.Id == id).AnyAsync();
        }

        public async Task<ResponseEntity<InfoQuestionEntity>> GetAll(int page, int pageSize)
        {
            var totalRecords = await _collection.CountDocumentsAsync(_ => true);
            var info = await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var resp = new ResponseEntity<InfoQuestionEntity>("Informacion de preguntas obtenida", info);
            resp.totalPages = totalPages;
            resp.totalRecords = (int)totalRecords;
            return resp;
        }

        public async Task<ResponseEntity<InfoQuestionEntity>> GetAll(DateTime lastSyncDate)
        {
            try
            {
                var filter = Builders<InfoQuestionEntity>.Filter.Gt(s => s.DateUpdate, lastSyncDate);
                var info = await _collection.Find(filter).ToListAsync();

                if (info == null || !info.Any())
                {
                    return new ResponseEntity<InfoQuestionEntity>($"No se encontraron contextos de pregunta actualizados desde {lastSyncDate}", false);
                    
                }

                return new ResponseEntity<InfoQuestionEntity>($"Se encontraron {info.Count} contextos de pregunta actualizados desde {lastSyncDate}", info);
                
            }
            catch (Exception ex)
            {
                return new ResponseEntity<InfoQuestionEntity>($"Error: {ex.Message}", false);
            }
        }

        public async Task<InfoQuestionEntity?> GetById(string id)
        {
            return await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }



    }
}