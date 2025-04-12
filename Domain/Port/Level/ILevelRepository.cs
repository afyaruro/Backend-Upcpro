
using Domain.Base.ResponseEntity;
using Domain.Entity.Level;

namespace Domain.Port.Level
{
    public interface ILevelRepository
    {
        Task<LevelEntity> GetById(string id);
        Task<bool> ExistById(string id);
        Task<LevelEntity> ExistByLevel(int level, string competenceId);
        Task<LevelEntity> Add(LevelEntity entity);
        Task<bool> Update(LevelEntity entity);
        Task<bool> Delete(string id);
        Task<ResponseEntity<LevelEntity>> GetAll(int page, int pageSize);
    }
}