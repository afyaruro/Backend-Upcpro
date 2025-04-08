
using Domain.Base.ResponseEntity;
using Domain.Entity.Level;
using Domain.Port.Level;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.ResultLevel
{
    public class ResultLevelRepository : IResultLevelRepository
    {
        private readonly IMongoCollection<ResultLevelEntity> _collection;


        public ResultLevelRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<ResultLevelEntity>("ResultLevel");


        }

        public async Task<ResultLevelEntity> Add(ResultLevelEntity entity)
        {
            await _collection.InsertOneAsync(entity);

            return entity;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _collection.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> ExistById(string id)
        {
            return await _collection.Find(c => c.Id == id).AnyAsync();
        }


        public async Task<ResponseEntity<ResultLevelEntity>> GetAll(int page, int pageSize)
        {
            var totalRecords = await _collection.CountDocumentsAsync(_ => true);
            var ResultLevels = await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var resp = new ResponseEntity<ResultLevelEntity>("Niveles Obtenidos", ResultLevels)
            {
                totalPages = totalPages,
                totalRecords = (int)totalRecords
            };

            return resp;
        }

        public async Task<ResultLevelEntity> GetById(string id)
        {
            var ResultLevel = await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
            return ResultLevel;
        }

        public async Task<bool> Update(ResultLevelEntity entity)
        {
            var result = await _collection.ReplaceOneAsync(c => c.Id == entity.Id, entity);
            return result.MatchedCount > 0;
        }

 
    }
}