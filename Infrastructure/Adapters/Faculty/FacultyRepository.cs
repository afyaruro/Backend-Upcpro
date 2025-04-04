using Domain.Base.ResponseEntity;
using Domain.Entity.Facultad;
using Domain.Port.Faculty;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.Faculty
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly IMongoCollection<FacultyEntity> _collection;

        public FacultyRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<FacultyEntity>("Faculty");

        }

        public async Task<FacultyEntity> Add(FacultyEntity entity)
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

        public async Task<bool> ExistByName(string name)
        {
            return await _collection.Find(c => c.Name == name).AnyAsync();
        }

        public async Task<ResponseEntity<FacultyEntity>> GetAll(int page, int pageSize)
        {
            var totalRecords = await _collection.CountDocumentsAsync(_ => true);
            var Faculties = await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var resp = new ResponseEntity<FacultyEntity>("Facultades Obtenidas", Faculties);
            resp.totalPages = totalPages;
            resp.totalRecords = (int)totalRecords;
            return resp;
        }

        public async Task<bool> Update(FacultyEntity entity)
        {
            var result = await _collection.ReplaceOneAsync(c => c.Id == entity.Id, entity);
            return result.MatchedCount > 0;
        }
    }
}