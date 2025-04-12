
using Domain.Base.ResponseEntity;
using Domain.Entity.Level;
using Domain.Port.Level;
using FluentValidation;

namespace Application.Service.ResultLevel.Commands.GetRanking
{
    public class RankingCommandHandler
    {
        private readonly IResultLevelRepository _resultLevelRepository;

        public RankingCommandHandler(IResultLevelRepository resultLevelRepository)
        {
            this._resultLevelRepository = resultLevelRepository;
        }

        public async Task<(List<ResultLevelEntity>, int)> HandleAsync(RankingInputCommand command, string IdUser)
        {

            var validator = new RankingCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await this._resultLevelRepository.Ranking(idCompetence: command.IdCompetence, idUser: IdUser);
        }

        
    }
}

