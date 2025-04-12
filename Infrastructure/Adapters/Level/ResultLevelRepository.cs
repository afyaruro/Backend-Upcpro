
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
            _collection = context._dbs.GetCollection<ResultLevelEntity>("Result_Level");


        }

        public async Task<ResultLevelEntity> Add(ResultLevelEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<bool> Exist(string userId, string idCompetence)
        {
            return await _collection.Find(c => c.UserId == userId && c.IdCompetence == idCompetence).AnyAsync();
        }


        public async Task<bool> ExistById(string id)
        {
            return await _collection.Find(c => c.Id == id).AnyAsync();
        }

        public Task<bool> ExistLevel(string userId, string idCompetence, string levelId)
        {
            return _collection.Find(c => c.UserId == userId && c.IdCompetence == idCompetence && c.PassedLevels.Contains(levelId)).AnyAsync();
        }

        public async Task<ResultLevelEntity> GetById(string id)
        {
            var ResultLevel = await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
            return ResultLevel;
        }

        public async Task<ResultLevelEntity> GetResultLevel(string userId, string idCompetence)
        {
            var ResultLevel = await _collection.Find(c => c.UserId == userId && c.IdCompetence == idCompetence).FirstOrDefaultAsync();
            return ResultLevel;
        }

        public async Task<(List<ResultLevelEntity>, int)> Ranking(string idCompetence, string idUser)
        {
            var filter = Builders<ResultLevelEntity>.Filter.Eq(c => c.IdCompetence, idCompetence);
            var sort = Builders<ResultLevelEntity>.Sort.Descending(c => c.Score);

            var topRankings = await _collection.Find(filter)
                                                .Sort(sort)
                                                .Limit(10)
                                                .ToListAsync();

            var allRankings = await _collection.Find(filter)
                                                .Sort(sort)
                                                .ToListAsync();

            var userPosition = allRankings.FindIndex(c => c.UserId == idUser) + 1;

            return (topRankings, userPosition);

        }

        public async Task<bool> Update(string levelId, double score, string id)
        {
            var update = Builders<ResultLevelEntity>.Update
                            .AddToSet(c => c.PassedLevels, levelId)
                            .Set(c => c.Score, score).Set(c => c.DateUpdate, DateTime.Now);

            var result = await _collection.UpdateOneAsync(c => c.Id == id, update); return result.MatchedCount > 0;
        }

        public async Task<ResponseEntity<ResultLevelEntity>> GetAllUser(string userId)
        {
            var result = await _collection.Find(c => c.UserId == userId).ToListAsync();
            if (result.Count == 0)
                return new ResponseEntity<ResultLevelEntity>("No se encontraron resultados", false);
            return new ResponseEntity<ResultLevelEntity>("Resultados encontrados", result);
        }

    }
}