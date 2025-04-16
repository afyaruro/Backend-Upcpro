
using Application.Base.Validate;
using Application.Service.Competence.Commands.CompetenceCreate;
using Application.Service.Competence.Commands.CompetenceDelete;
using Application.Service.Competence.Commands.CompetenceGetAllPage;
using Application.Service.Competence.Commands.CompetenceUpdate;
using Application.Service.Faculty.Commands.FacultyGetAllPage;
using Domain.Base.ResponseEntity;
using Domain.Port.Competence;
using Microsoft.AspNetCore.Http;

namespace Application.Service.Competence
{
    public class CompetenceService
    {
        private readonly ICompetenceRepository _repository;
        public CompetenceService(ICompetenceRepository repository) => _repository = repository;

        public async Task<CompetenceOutputCreateCommand> Create(CompetenceCreateInputCommand command)
        {
            var _create = new CompetenceCreateCommandHandler(_repository);
            return await _create.HandleAsync(command);
        }

        public async Task<ResponseEntity<CompetenceGetAllPageOutputCommand>> GetAllPage(CompetenceGetAllPageInputCommand command)
        {
            var _getAll = new CompetenceGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> Update(CompetenceUpdateInputCommand command)
        {
            var _update = new CompetenceUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<bool> Delete(CompetenceDeleteInputCommand command)
        {
            var _delete = new CompetenceDeleteCommandHandler(_repository);
            return await _delete.HandleAsync(command);
        }

        public async Task<ResponseEntity<CompetenceGetAllPageOutputCommand>> GetAllSync(CompetenceGetAllPageSyncInputCommand command)
        {
            var _getAll = new CompetenceGetAllPageSyncCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        


    }
}