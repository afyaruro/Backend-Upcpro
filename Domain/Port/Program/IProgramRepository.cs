using Domain.Base.ResponseEntity;
using Domain.Entity.Program;

namespace Domain.Port.Program
{
    public interface IProgramRepository
    {
        Task<bool> ExistById(string id);
        Task<bool> ExistByName(string name);
        Task<ProgramEntity> ByName(string name);
        Task<ResponseEntity<ProgramEntity>> GetAll(int page, int size);
        Task<ResponseEntity<ProgramEntity>> GetAll(DateTime lastSyncDate);

    }
}