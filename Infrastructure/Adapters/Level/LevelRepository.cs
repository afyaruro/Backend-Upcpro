
using Domain.Base.ResponseEntity;
using Domain.Entity.Level;
using Domain.Port.Level;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.Level
{
    public class LevelRepository : ILevelRepository
    {
        private readonly IMongoCollection<LevelEntity> _collection;


        public LevelRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<LevelEntity>("Level");


        }

        public async Task<LevelEntity> Add(LevelEntity entity)
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

        public async Task<LevelEntity> ExistByLevel(int level)
        {
            return await _collection.Find(c => c.Level == level).FirstOrDefaultAsync();
        }

        public async Task<ResponseEntity<LevelEntity>> GetAll(int page, int pageSize)
        {
            var totalRecords = await _collection.CountDocumentsAsync(_ => true);
            var Levels = await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var resp = new ResponseEntity<LevelEntity>("Niveles Obtenidos", Levels)
            {
                totalPages = totalPages,
                totalRecords = (int)totalRecords
            };

            return resp;
        }

        public async Task<LevelEntity> GetById(string id)
        {
            var level = await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
            return level;
        }

        public async Task<bool> Update(LevelEntity entity)
        {
            var result = await _collection.ReplaceOneAsync(c => c.Id == entity.Id, entity);
            return result.MatchedCount > 0;
        }


    }
}