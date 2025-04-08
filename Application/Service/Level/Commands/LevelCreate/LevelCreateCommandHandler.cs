
using Application.Common.Exceptions;
using Domain.Entity.Level;
using Domain.Port.Level;
using FluentValidation;

namespace Application.Service.Level.Commands.LevelCreate
{
    public class LevelCreateCommandHandler
    {
        private readonly ILevelRepository _LevelRepository;

        public LevelCreateCommandHandler(ILevelRepository LevelRepository)
        {
            this._LevelRepository = LevelRepository;
        }

        public async Task<LevelOutputCreateCommand> HandleAsync(LevelCreateInputCommand command)
        {
            var validator = new LevelCreateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            if (await _LevelRepository.ExistByLevel(command.Level) != null)
            {
                throw new EntityExistException("El nivel ya existe");
            }


            var resp = await this._LevelRepository.Add(new LevelEntity(level: command.Level, dificulty: command.Dificulty, reward: command.Reward, numQuestion: command.NumQuestion));

            return new LevelOutputCreateCommand(level: resp.Level, dificulty: resp.Dificulty, reward: resp.Reward, numQuestion: resp.NumQuestion, id: resp.Id);
        }
    }
}

