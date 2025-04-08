
using Application.Common.Exceptions;
using Domain.Port.Competence;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceDelete
{
    public class CompetenceDeleteCommandHandler
    {
        private readonly ICompetenceRepository _competenceRepository;

        public CompetenceDeleteCommandHandler(ICompetenceRepository competenceRepository)
        {
            this._competenceRepository = competenceRepository;
        }

        public async Task<bool> HandleAsync(CompetenceDeleteInputCommand command)
        {

            var validator = new CompetenceDeleteCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (!await _competenceRepository.ExistById(command.Id))
            {
                throw new EntityNotFoundException("La competencia no existe");
            }

            return await this._competenceRepository.Delete(command.Id);
        }
    }
}

