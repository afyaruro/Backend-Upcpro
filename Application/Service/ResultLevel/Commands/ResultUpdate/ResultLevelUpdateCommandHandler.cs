
using Application.Common.Exceptions;
using Domain.Entity.Level;
using Domain.Port.Level;
using FluentValidation;

namespace Application.Service.ResultLevel.Commands.ResultLevelUpdate
{
    public class ResultLevelUpdateCommandHandler
    {
        private readonly IResultLevelRepository _resultLevelRepository;

        public ResultLevelUpdateCommandHandler(IResultLevelRepository resultLevelRepository)
        {
            this._resultLevelRepository = resultLevelRepository;
        }

        public async Task<bool> HandleAsync(ResultLevelUpdateInputCommand command, string userId)
        {

            var validator = new ResultLevelUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var ResultLevel = await _resultLevelRepository.GetResultLevel(userId: userId, idCompetence: command.IdCompetence);
            if (ResultLevel == null)
            {
                var resultLevel = new ResultLevelEntity
                    {
                        UserId = userId,
                        IdCompetence = command.IdCompetence,
                        Score = 0,
                        PassedLevels = new List<string>()
                    };

                   ResultLevel = await this._resultLevelRepository.Add(resultLevel);
                   
            }

            var existLevel = await this._resultLevelRepository.ExistLevel(userId: userId, idCompetence: command.IdCompetence, levelId: command.LevelId);
            
            if (!existLevel)
            {
                throw new EntityNotFoundException($"El nivel con id {command.LevelId} no existe");
            }

            return await this._resultLevelRepository.Update(levelId: command.LevelId, score: command.Score, id: ResultLevel.Id);
        }
    }
}

