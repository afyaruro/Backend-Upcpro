
using Application.Common.Exceptions;
using Domain.Entity.Competence;
using Domain.Port.Competence;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceDelete
{
    public class DeleteCompetenceCommandHandler
    {
        private readonly ICompetenceRepository _competenceRepository;

        public DeleteCompetenceCommandHandler(ICompetenceRepository competenceRepository)
        {
            this._competenceRepository = competenceRepository;
        }

        public async Task<bool> HandleAsync(DeleteCompetenceCommand command)
        {

            var validator = new CompetenceDelete.DeleteCompetenceCommandValidator();
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

