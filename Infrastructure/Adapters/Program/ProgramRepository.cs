
using Domain.Base.ResponseEntity;
using Domain.Entity.Facultad;
using Domain.Entity.Program;
using Domain.Port.Program;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.Program
{
    public class ProgramRepository : IProgramRepository
    {

        private readonly IMongoCollection<ProgramEntity> _collection;
        private readonly IMongoCollection<FacultyEntity> _collectionFaculty;

        public ProgramRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<ProgramEntity>("Program");
            _collectionFaculty = context._dbs.GetCollection<FacultyEntity>("Faculty");

        }

        public async Task<ProgramEntity> Add(ProgramEntity entity)
        {
            await _collection.InsertOneAsync(entity);

            entity.Faculty = await _collectionFaculty
                          .Find(f => f.Id == entity.IdFaculty)
                          .FirstOrDefaultAsync();

            return entity;
        }

        public async Task<ProgramEntity> ByName(string name)
        {
            return await _collection.Find(c => c.Name == name).FirstOrDefaultAsync();
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

        public async Task<ResponseEntity<ProgramEntity>> GetAll(int page, int pageSize)
        {
            var totalRecords = await _collection.CountDocumentsAsync(_ => true);
            var programs = await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            foreach (var program in programs)
            {
                program.Faculty = await _collectionFaculty
                    .Find(f => f.Id == program.IdFaculty)
                    .FirstOrDefaultAsync();
            }

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var resp = new ResponseEntity<ProgramEntity>("Programas Obtenidos", programs)
            {
                totalPages = totalPages,
                totalRecords = (int)totalRecords
            };

            return resp;
        }

        public async Task<ResponseEntity<ProgramEntity>> GetAll(DateTime lastSyncDate)
        {
            try
            {
                var filter = Builders<ProgramEntity>.Filter.Gt(s => s.DateUpdate, lastSyncDate);
                var programas = await _collection.Find(filter).ToListAsync();

                if (programas == null || !programas.Any())
                {
                    return new ResponseEntity<ProgramEntity>($"No se encontraron programas actualizados desde {lastSyncDate}", false);
                    
                }

                return new ResponseEntity<ProgramEntity>($"Se encontraron {programas.Count} programas actualizados desde {lastSyncDate}", programas);
                
            }
            catch (Exception ex)
            {
                return new ResponseEntity<ProgramEntity>($"Error: {ex.Message}", false);
            }
        }

        public async Task<bool> Update(ProgramEntity entity)
        {
            var result = await _collection.ReplaceOneAsync(c => c.Id == entity.Id, entity);
            return result.MatchedCount > 0;
        }
    }
}