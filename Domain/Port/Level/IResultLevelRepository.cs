
using Domain.Base.ResponseEntity;
using Domain.Entity.Level;

namespace Domain.Port.Level
{
    public interface IResultLevelRepository
    {
        Task<ResultLevelEntity> GetById(string id);
        Task<ResultLevelEntity> GetResultLevel(string userId, string idCompetence);
        Task<bool> ExistById(string id);
        Task<bool> Exist(string userId, string idCompetence);
        Task<bool> ExistLevel(string userId, string idCompetence, string levelId);
        Task<ResultLevelEntity> Add(ResultLevelEntity entity);
        Task<(List<ResultLevelEntity>, int)> Ranking(string idCompetence, string idUser);
        Task<bool> Update(string levelId, double score, string id);
        Task<ResponseEntity<ResultLevelEntity>> GetAllUser(string userId);

        

        
    }
}