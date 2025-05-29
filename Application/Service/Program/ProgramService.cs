
using Application.Service.Program.Commands.ProgramGetAllPage;
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



        public async Task<ResponseEntity<ProgramGetAllPageOutputCommand>> GetAllPage(ProgramGetAllPageInputCommand command)
        {
            var _getAll = new ProgramGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }



        public async Task<ResponseEntity<ProgramGetAllPageOutputCommand>> GetAllSync(ProgramGetAllPageSyncInputCommand command)
        {
            var _getAll = new ProgramGetAllPageSyncCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }


    }
}