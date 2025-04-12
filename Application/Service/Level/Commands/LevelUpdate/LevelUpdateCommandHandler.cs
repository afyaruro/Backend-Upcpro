
using Application.Base.Validate;
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

            var Level = new LevelEntity(level: command.Level, dificulty: command.Dificulty, reward: command.Reward, questions: command.Questions, idCompetence: command.IdCompetence);
            Level.Id = command.Id;
            Level.DateUpdate = DateTime.Now;

            var exist = await _LevelRepository.ExistByLevel(level: command.Level, competenceId: command.IdCompetence);
            if (exist != null)
            {
                if (exist.Id != Level.Id)
                {
                    throw new EntityExistException("El nuevo nivel de la competencia a actualizar ya existe");
                }
                else
                {
                    List<string> questionsValidate = [];
                    foreach (var questionId in command.Questions)
                    {
                        if (IsValidObjectId.IsValid(questionId))
                        {
                            questionsValidate.Add(questionId);
                        }
                    }

                    Level.Questions = questionsValidate;
                }

            }

            return await this._LevelRepository.Update(Level);
        }
    }
}

