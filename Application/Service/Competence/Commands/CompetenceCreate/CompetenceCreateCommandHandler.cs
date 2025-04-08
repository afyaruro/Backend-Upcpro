
using Application.Common.Exceptions;
using Domain.Entity.Competence;
using Domain.Port.Competence;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceCreate
{
    public class CompetenceCreateCommandHandler
    {
        private readonly ICompetenceRepository _competenceRepository;

        public CompetenceCreateCommandHandler(ICompetenceRepository competenceRepository)
        {
            this._competenceRepository = competenceRepository;
        }

        public async Task<CompetenceOutputCreateCommand> HandleAsync(CompetenceCreateInputCommand command)
        {

            var validator = new CompetenceCreateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            command.Name = command.Name.ToUpper();

            if (await _competenceRepository.ExistByName(command.Name) != null)
            {
                throw new EntityExistException("La competencia ya existe");
            }

            var resp = await this._competenceRepository.Add(new CompetenceEntity(name: command.Name));

            return new CompetenceOutputCreateCommand(resp.Name, resp.Id);
        }
    } 
}

