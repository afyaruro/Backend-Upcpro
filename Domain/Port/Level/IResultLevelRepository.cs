
using Domain.Base.ResponseEntity;
using Domain.Entity.Level;

namespace Domain.Port.Level
{
    public interface IResultLevelRepository
    {
        Task<ResultLevelEntity> GetById(string id);
        Task<bool> ExistById(string id);
        Task<ResultLevelEntity> Add(ResultLevelEntity entity);
        Task<bool> Update(ResultLevelEntity entity);
        Task<bool> Delete(string id);
        Task<ResponseEntity<ResultLevelEntity>> GetAll(int page, int pageSize);
        
    }
}