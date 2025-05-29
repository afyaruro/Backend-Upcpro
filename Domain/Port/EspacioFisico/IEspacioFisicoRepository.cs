
using Domain.Base.ResponseEntity;
using Domain.Entity.EspacioFisico;

namespace Domain.Port.EspacioFisico
{
    public interface IEspacioFisicoRepository
    {
        Task<bool> ExistById(string id);
        Task<bool> ExistByName(string name);
        Task<ResponseEntity<EspacioFisicoEntity>> GetAll(int page, int size);
        Task<ResponseEntity<EspacioFisicoEntity>> GetAll(DateTime lastSyncDate);


    }
}