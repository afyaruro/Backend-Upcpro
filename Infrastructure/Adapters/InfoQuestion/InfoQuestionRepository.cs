using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<InfoQuestionEntity> Add(InfoQuestionEntity entity)
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

        public async Task<InfoQuestionEntity?> GetById(string id)
        {
            return await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(InfoQuestionEntity entity)
        {
            var result = await _collection.ReplaceOneAsync(c => c.Id == entity.Id, entity);
            return result.ModifiedCount > 0;
        }



    }
}