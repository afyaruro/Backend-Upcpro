using Application.Service.Faculty.Commands.FacultyGetAllPage;
using Domain.Base.ResponseEntity;
using Domain.Port.Faculty;

namespace Application.Service.Faculty
{
    public class FacultyService
    {
        private readonly IFacultyRepository _repository;
        public FacultyService(IFacultyRepository repository) => _repository = repository;


        public async Task<ResponseEntity<FacultyGetAllPageOutputCommand>> GetAllPage(FacultyGetAllPageInputCommand command)
        {
            var _getAll = new FacultyGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }


        public async Task<ResponseEntity<FacultyGetAllPageOutputCommand>> GetAllSync(FacultyGetAllPageSyncInputCommand command)
        {
            var _getAll = new FacultyGetAllPageSyncCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }


    }
}