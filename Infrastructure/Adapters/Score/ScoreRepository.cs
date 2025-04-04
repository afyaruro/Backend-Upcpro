using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Base.ResponseEntity;
using Domain.Entity;
using Domain.Entity.Score;
using Domain.Port.Score;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.Score
{
    public class ScoreRepository : IScoreRepository
    {

        private readonly IMongoCollection<ScoreEntity> _collection;

        public ScoreRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<ScoreEntity>("SCORE");
        }
        public async Task<bool> Add(ScoreEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return true;
        }

        private async Task<ResponseRankingEntity> GetRanking(string field, string userId)
        {
            var totalRecords = await _collection.CountDocumentsAsync(_ => true);
            var top10 = await _collection.Find(_ => true)
                .Sort(Builders<ScoreEntity>.Sort.Descending(field))
                .Limit(10)
                .ToListAsync();

            var userScore = await _collection.Find(s => s.userId == userId).FirstOrDefaultAsync();

            var response = new ResponseRankingEntity("Ranking obtenido", top10);
            response.totalRecords = (int)totalRecords;
            var userRank = userScore != null
            ? (int)(await _collection.CountDocumentsAsync(s =>
                (double)s.GetType().GetProperty(field)!.GetValue(s)! >
                (double)userScore.GetType().GetProperty(field)!.GetValue(userScore)!)) + 1
            : -1;

            if (field == "scoreCiudadanas")
            {
                response.score = userScore!.scoreCiudadanas;
            }

            if (field == "scoreLectura")
            {
                response.score = userScore!.scoreLectura;
            }

            if (field == "scoreRazonamiento")
            {
                response.score = userScore!.scoreRazonamiento;
            }

            if (field == "scoreIngles")
            {
                response.score = userScore!.scoreIngles;
            }

            return response;
        }

        public async Task<ScoreEntity?> GetScoreUser(string userId)
        {
            return await _collection.Find(s => s.userId == userId).FirstOrDefaultAsync();
        }

        public Task<ResponseRankingEntity> RankinCompetenciaCiudadana(string userId)
        {
            return GetRanking("scoreCiudadanas", userId);
        }

        public Task<ResponseRankingEntity> RankingLecturaCritica(string userId)
        {
            return GetRanking("scoreLectura", userId);

        }

        public Task<ResponseRankingEntity> RankingRazonamientoCuantitativo(string userId)
        {
            return GetRanking("scoreRazonamiento", userId);

        }


        public Task<ResponseRankingEntity> RankingIngles(string userId)
        {
            return GetRanking("scoreIngles", userId);
        }



        public Task<bool> UpdateCompetenciaCiudadana(string userId, int puntaje)
        {
            return UpdateScore(userId, "scoreCiudadanas", puntaje);

        }


        public Task<bool> UpdateIngles(string userId, int puntaje)
        {
            return UpdateScore(userId, "scoreIngles", puntaje);
        }


        public Task<bool> UpdateLecturaCritica(string userId, int puntaje)
        {
            return UpdateScore(userId, "scoreLectura", puntaje);
        }


        public Task<bool> UpdateRazonamientoCuantitativo(string userId, int puntaje)
        {
            return UpdateScore(userId, "scoreRazonamiento", puntaje);
        }

        private async Task<bool> UpdateScore(string userId, string field, double score)
        {
            var filter = Builders<ScoreEntity>.Filter.Eq(s => s.userId, userId);
            var update = Builders<ScoreEntity>.Update.Set(field, score);
            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }


    }
}