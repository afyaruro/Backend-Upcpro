using Application.Service.Faculty.Commands.FacultyCreate;
using Application.Service.Faculty.Commands.FacultyDelete;
using Application.Service.Faculty.Commands.FacultyGetAllPage;
using Application.Service.Faculty.Commands.FacultyUpdate;
using Domain.Base.ResponseEntity;
using Domain.Port.Faculty;

namespace Application.Service.Faculty
{
    public class FacultyService
    {
        private readonly IFacultyRepository _repository;
        public FacultyService(IFacultyRepository repository) => _repository = repository;

        public async Task<FacultyCreateOutputCommand> Create(FacultyCreateInputCommand command)
        {
            var _create = new FacultyCreateCommandHandler(_repository);
            return await _create.HandleAsync(command);
        }

        public async Task<ResponseEntity<FacultyGetAllPageOutputCommand>> GetAllPage(FacultyGetAllPageInputCommand command)
        {
            var _getAll = new FacultyGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> Update(FacultyUpdateInputCommand command)
        {
            var _update = new FacultyUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<bool> Delete(FacultyDeleteInputCommand command)
        {
            var _delete = new FacultyDeleteCommandHandler(_repository);
            return await _delete.HandleAsync(command);
        }


    }
}