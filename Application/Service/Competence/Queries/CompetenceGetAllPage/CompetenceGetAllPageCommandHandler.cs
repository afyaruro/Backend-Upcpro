
using Domain.Base.ResponseEntity;
using Domain.Entity.Competence;
using Domain.Port.Competence;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class CompetenceGetAllPageCommandHandler
    {
        private readonly ICompetenceRepository _competenceRepository;

        public CompetenceGetAllPageCommandHandler(ICompetenceRepository competenceRepository)
        {
            this._competenceRepository = competenceRepository;
        }

        public async Task<ResponseEntity<CompetenceGetAllPageOutputCommand>> HandleAsync(CompetenceGetAllPageInputCommand command)
        {

            var validator = new CompetenceGetAllPageCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._competenceRepository.GetAll(page: command.Page, size: command.Size));
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
                var command = new CompetenceGetAllPageOutputCommand(name: entity.Name, id: entity.Id);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

