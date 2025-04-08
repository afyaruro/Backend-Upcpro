
using Application.Common.Exceptions;
using Domain.Entity.Level;
using Domain.Port.Level;
using FluentValidation;

namespace Application.Service.Level.Commands.LevelUpdate
{
    public class LevelUpdateCommandHandler
    {
        private readonly ILevelRepository _LevelRepository;

        public LevelUpdateCommandHandler(ILevelRepository LevelRepository)
        {
            this._LevelRepository = LevelRepository;
        }

        public async Task<bool> HandleAsync(LevelUpdateInputCommand command)
        {

            var validator = new LevelUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var Level = new LevelEntity(level: command.Level, dificulty: command.Dificulty, reward: command.Reward, numQuestion: command.NumQuestion);
            Level.Id = command.Id;
            Level.DateUpdate = DateTime.Now;

            if(!await _LevelRepository.ExistById(Level.Id)){
                throw new EntityNotFoundException("La competencia a actualizar no existe");
            }

            var exist = await _LevelRepository.ExistByLevel(Level.Level);
            if (exist != null)
            {
                if(exist.Id != Level.Id){
                    throw new EntityExistException("El nuevo nivel a actualizar ya existe");
                }

            } 

            return await this._LevelRepository.Update(Level);
        }
    }
}

