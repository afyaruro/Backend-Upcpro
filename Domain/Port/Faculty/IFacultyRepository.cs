
using Domain.Base.ResponseEntity;
using Domain.Entity.Facultad;

namespace Domain.Port.Faculty
{
    public interface IFacultyRepository
    {
        Task<bool> ExistById(string id);
        Task<bool> ExistByName(string name);
        Task<ResponseEntity<FacultyEntity>> GetAll(int page, int size);
        Task<ResponseEntity<FacultyEntity>> GetAll(DateTime lastSyncDate);


    }
}