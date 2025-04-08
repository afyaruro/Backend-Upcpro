using Application.Service.Program.Commands.ProgramCreate;
using Application.Service.Program.Commands.ProgramDelete;
using Application.Service.Program.Commands.ProgramGetAllPage;
using Application.Service.Program.Commands.ProgramUpdate;
using Domain.Base.ResponseEntity;
using Domain.Port.Faculty;
using Domain.Port.Program;

namespace Application.Service.Program
{
    public class ProgramService
    {
        private readonly IProgramRepository _repository;
        private readonly IFacultyRepository _repositoryFaculty;

        public ProgramService(IProgramRepository repository, IFacultyRepository repositoryFaculty)
        {
            this._repository = repository;
            this._repositoryFaculty = repositoryFaculty;
        }

        public async Task<ProgramCreateOutputCommand> Create(ProgramCreateInputCommand command)
        {
            var _create = new ProgramCreateCommandHandler(_repository, _repositoryFaculty);
            return await _create.HandleAsync(command);
        }

        public async Task<ResponseEntity<ProgramGetAllPageOutputCommand>> GetAllPage(ProgramGetAllPageInputCommand command)
        {
            var _getAll = new ProgramGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> Update(ProgramUpdateInputCommand command)
        {
            var _update = new ProgramUpdateCommandHandler(_repository, _repositoryFaculty);
            return await _update.HandleAsync(command);
        }

        public async Task<bool> Delete(ProgramDeleteInputCommand command)
        {
            var _delete = new ProgramDeleteCommandHandler(_repository);
            return await _delete.HandleAsync(command);
        }


    }
}