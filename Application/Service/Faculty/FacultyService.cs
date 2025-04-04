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

        public async Task<CreateOutputFacultyCommand> Create(CreateInputFacultyCommand command)
        {
            var _create = new CreateFacultyCommandHandler(_repository);
            return await _create.HandleAsync(command);
        }

        public async Task<ResponseEntity<GetAllPageFacultyOutputCommand>> GetAllPage(GetAllPageFacultyInputCommand command)
        {
            var _getAll = new GetAllPageFacultyCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> Update(UpdateFacultyCommand command)
        {
            var _update = new UpdateFacultyCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<bool> Delete(DeleteFacultyCommand command)
        {
            var _delete = new DeleteFacultyCommandHandler(_repository);
            return await _delete.HandleAsync(command);
        }


    }
}