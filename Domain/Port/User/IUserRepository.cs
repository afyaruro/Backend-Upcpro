
using Domain.Base.ResponseEntity;
using Domain.Entity;
using Domain.Entity.RankingResponseEntity;

namespace Domain.Port.User
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByMail(string mail);
        Task<UserEntity?> GetById(string id);
        Task<bool> ExistByMail(string mail);
        Task<bool> ExistById(string id);
        Task<UserEntity?> IsUserType(string id, string type);
        Task<UserEntity> Add(UserEntity entity);
        Task<bool> Update(UserEntity entity);
        Task<bool> UpdatePassword(string userId, string password);
        Task<bool> UpdateMail(string userId, string mail);
        Task<ResponseEntity<UserEntity>> GetAll(int page, int pageSize);
        Task<bool> UpdatePuntaje(string userId, int puntaje);
        Task<RankingResponseEntity<UserEntity>> GetRankingByScore(string userId);
        


    }
}