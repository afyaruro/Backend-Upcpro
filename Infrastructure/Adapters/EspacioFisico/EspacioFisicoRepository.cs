using Domain.Base.ResponseEntity;
using Domain.Entity.EspacioFisico;
using Domain.Port.EspacioFisico;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.EspacioFisico
{
    public class EspacioFisicoRepository : IEspacioFisicoRepository
    {
        private readonly IMongoCollection<EspacioFisicoEntity> _collection;

        public EspacioFisicoRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<EspacioFisicoEntity>("EspacioFisico");

        }

        public async Task<bool> ExistById(string id)
        {
            return await _collection.Find(c => c.Id == id).AnyAsync();
        }

        public async Task<bool> ExistByName(string name)
        {
            return await _collection.Find(c => c.Name == name).AnyAsync();
        }

        public async Task<ResponseEntity<EspacioFisicoEntity>> GetAll(int page, int pageSize)
        {
            var totalRecords = await _collection.CountDocumentsAsync(_ => true);
            var Faculties = await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var resp = new ResponseEntity<EspacioFisicoEntity>("EspacioFisicos Obtenidas", Faculties);
            resp.totalPages = totalPages;
            resp.totalRecords = (int)totalRecords;
            return resp;
        }

        public async Task<ResponseEntity<EspacioFisicoEntity>> GetAll(DateTime lastSyncDate)
        {
            try
            {
                var filter = Builders<EspacioFisicoEntity>.Filter.Gt(s => s.DateUpdate, lastSyncDate);
                var facultad = await _collection.Find(filter).ToListAsync();

                if (facultad == null || !facultad.Any())
                {
                    return new ResponseEntity<EspacioFisicoEntity>($"No se encontraron EspacioFisicos actualizadas desde {lastSyncDate}", false);
                    
                }

                return new ResponseEntity<EspacioFisicoEntity>($"Se encontraron {facultad.Count} EspacioFisicos actualizadas desde {lastSyncDate}", facultad);
                
            }
            catch (Exception ex)
            {
                return new ResponseEntity<EspacioFisicoEntity>($"Error: {ex.Message}", false);
            }
        }

       
    }
}