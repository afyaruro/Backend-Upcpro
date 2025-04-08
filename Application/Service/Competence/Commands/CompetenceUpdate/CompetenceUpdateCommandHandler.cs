
using Application.Common.Exceptions;
using Domain.Entity.Competence;
using Domain.Port.Competence;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceUpdate
{
    public class CompetenceUpdateCommandHandler
    {
        private readonly ICompetenceRepository _competenceRepository;

        public CompetenceUpdateCommandHandler(ICompetenceRepository competenceRepository)
        {
            this._competenceRepository = competenceRepository;
        }

        public async Task<bool> HandleAsync(CompetenceUpdateInputCommand command)
        {

            var validator = new CompetenceUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var competence = new CompetenceEntity(name: command.Name.ToUpper());

            competence.Id = command.Id;
            competence.DateUpdate = DateTime.Now;

            if(!await _competenceRepository.ExistById(competence.Id)){
                throw new EntityNotFoundException("La competencia a actualizar no existe");
            }

            var exist = await _competenceRepository.ExistByName(competence.Name);
            if (exist != null)
            {
                if(exist.Id != competence.Id){
                    throw new EntityExistException("El nuevo nombre de la competencia a actualizar ya existe");
                }

                throw new EntityExistException("Este es el nombre actual de la competencia");
            } 

            return await this._competenceRepository.Update(competence);
        }
    }
}

