using Domain.Base.ResponseEntity;

namespace Domain.Port
{
    public interface IQuestionRepository<E>
    {
        Task<E?> GetById(string id);
        Task<bool> ExistById(string id);
        Task<E> Add(E entity);
        Task<bool> Update(E entity);
        Task<bool> Delete(string id);
        Task<ResponseEntity<E>> GetAll(int page, int pageSize);

    }
}