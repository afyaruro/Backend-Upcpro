
using Application.Common.Exceptions;
using Domain.Entity.Competence;
using Domain.Port.Competence;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceUpdate
{
    public class UpdateCompetenceCommandHandler
    {
        private readonly ICompetenceRepository _competenceRepository;

        public UpdateCompetenceCommandHandler(ICompetenceRepository competenceRepository)
        {
            this._competenceRepository = competenceRepository;
        }

        public async Task<bool> HandleAsync(UpdateCompetenceCommand command)
        {

            var validator = new UpdateCompetenceCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var competence = new CompetenceEntity
            {
                Name = command.Name

            };

            competence.Id = command.Id;
            competence.Name = competence.Name.ToUpper();
            competence.DateUpdate = DateTime.Now;


            if (!await _competenceRepository.ExistById(competence.Id))
            {
                throw new EntityNotFoundException("La competencia a actualizar no existe");
            }

            if (await _competenceRepository.ExistByName(competence.Name))
            {
                throw new EntityExistException("El nuevo nombre de la competencia a actualizar ya existe");
            }

            return await this._competenceRepository.Update(competence);
        }
    }
}

