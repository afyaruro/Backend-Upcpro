using Domain.Base.ResponseEntity;

namespace Domain.Port
{
    public interface IQuestionRepository<E>
    {
        Task<E?> GetById(string id);
        Task<bool> ExistById(string id);
        Task<ResponseEntity<E>> GetAll(int page, int pageSize);
        Task<ResponseEntity<E>> GetAll(DateTime lastSyncDate);


    }
}