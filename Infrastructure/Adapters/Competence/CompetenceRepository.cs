
using Domain.Base.ResponseEntity;
using Domain.Entity.Competence;
using Domain.Port.Competence;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.Competence
{
    public class CompetenceRepository : ICompetenceRepository
    {

        private readonly IMongoCollection<CompetenceEntity> _collection;

        public CompetenceRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<CompetenceEntity>("Competence");

        }

        public async Task<CompetenceEntity> Add(CompetenceEntity entity)
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

        public async Task<CompetenceEntity> ExistByName(string name)
        {
            return await _collection.Find(c => c.Name== name).FirstOrDefaultAsync();
        }

        public async Task<ResponseEntity<CompetenceEntity>> GetAll(int page, int pageSize)
        {
            var totalRecords = await _collection.CountDocumentsAsync(_ => true);
            var competences = await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var resp = new ResponseEntity<CompetenceEntity>("Competencias Obtenidas", competences);
            resp.totalPages = totalPages;
            resp.totalRecords = (int)totalRecords;
            return resp;
        }

        public async Task<ResponseEntity<CompetenceEntity>> GetAll(DateTime lastSyncDate)
        {
            try
            {
                var filter = Builders<CompetenceEntity>.Filter.Gt(s => s.DateUpdate, lastSyncDate);
                var competencias = await _collection.Find(filter).ToListAsync();

                if (competencias == null || !competencias.Any())
                {
                    return new ResponseEntity<CompetenceEntity>($"No se encontraron competencias actualizadas desde {lastSyncDate}", false);
                    
                }

                return new ResponseEntity<CompetenceEntity>($"Se encontraron {competencias.Count} competencias actualizadas desde {lastSyncDate}", competencias);
                
            }
            catch (Exception ex)
            {
                return new ResponseEntity<CompetenceEntity>($"Error: {ex.Message}", false);
            }
        }

        public async Task<bool> Update(CompetenceEntity entity)
        {
            var result = await _collection.ReplaceOneAsync(c => c.Id == entity.Id, entity);
            return result.MatchedCount > 0;
        }
    }
}