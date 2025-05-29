
using Application.Service.Competence.Commands.CompetenceGetAllPage;
using Application.Service.Faculty.Commands.FacultyGetAllPage;
using Domain.Base.ResponseEntity;
using Domain.Port.Competence;
namespace Application.Service.Competence
{
    public class CompetenceService
    {
        private readonly ICompetenceRepository _repository;
        public CompetenceService(ICompetenceRepository repository) => _repository = repository;


        public async Task<ResponseEntity<CompetenceGetAllPageOutputCommand>> GetAllPage(CompetenceGetAllPageInputCommand command)
        {
            var _getAll = new CompetenceGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }


        public async Task<ResponseEntity<CompetenceGetAllPageOutputCommand>> GetAllSync(CompetenceGetAllPageSyncInputCommand command)
        {
            var _getAll = new CompetenceGetAllPageSyncCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }




    }
}