using Domain.Base.ResponseEntity;
using Domain.Entity.Program;

namespace Domain.Port.Program
{
    public interface IProgramRepository
    {
        Task<bool> ExistById(string id);
        Task<bool> ExistByName(string name);
        Task<ProgramEntity> ByName(string name);
        Task<ProgramEntity> Add(ProgramEntity entity);
        Task<bool> Update(ProgramEntity entity);
        Task<bool> Delete(string id);
        Task<ResponseEntity<ProgramEntity>> GetAll(int page, int size);
    }
}