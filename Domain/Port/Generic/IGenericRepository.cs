
using Domain.Base.ResponseEntity;

namespace Domain.Port.Generic
{
    public interface IGenericRepository<E>
    {
        Task<E?> GetById(string id);
        Task<E?> GetByName(string name);
        Task<bool> ExistById(string id);
        Task<bool> ExistByName(string name);
        Task<bool> Add(E entity);
        Task<bool> Update(E entity);
        Task<bool> Delete(string id);
        Task<ResponseEntity<E>> GetAll(int page, int pageSize);
    }
}