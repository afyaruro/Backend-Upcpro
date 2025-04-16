
using Application.Service.Faculty.Commands.FacultyGetAllPage;
using Domain.Base.ResponseEntity;
using Domain.Entity.Competence;
using Domain.Port.Competence;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceGetAllPage
{
    public class CompetenceGetAllPageSyncCommandHandler
    {
        private readonly ICompetenceRepository _CompetenceRepository;

        public CompetenceGetAllPageSyncCommandHandler(ICompetenceRepository CompetenceRepository)
        {
            this._CompetenceRepository = CompetenceRepository;
        }

        public async Task<ResponseEntity<CompetenceGetAllPageOutputCommand>> HandleAsync(CompetenceGetAllPageSyncInputCommand command)
        {

            var validator = new CompetenceGetAllPageSyncCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._CompetenceRepository.GetAll(command.LateDateSync));
        }

        private ResponseEntity<CompetenceGetAllPageOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<CompetenceEntity> resp)
        {
            var responseCommands = new ResponseEntity<CompetenceGetAllPageOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<CompetenceGetAllPageOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var command = new CompetenceGetAllPageOutputCommand(name: entity.Name, id: entity.Id, dateUpdate: entity.DateUpdate);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

