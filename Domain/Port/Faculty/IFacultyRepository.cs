
using Domain.Base.ResponseEntity;
using Domain.Entity.Facultad;

namespace Domain.Port.Faculty
{
    public interface IFacultyRepository
    {
        Task<bool> ExistById(string id);
        Task<bool> ExistByName(string name);
        Task<FacultyEntity> Add(FacultyEntity entity);
        Task<bool> Update(FacultyEntity entity);
        Task<bool> Delete(string id);
        Task<ResponseEntity<FacultyEntity>> GetAll(int page, int size);
    }
}