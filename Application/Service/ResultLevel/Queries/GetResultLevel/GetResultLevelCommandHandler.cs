
using Domain.Base.ResponseEntity;
using Domain.Entity.Level;
using Domain.Port.Level;
using FluentValidation;

namespace Application.Service.ResultLevel.Commands.GetResultLevel
{
    public class GetResultLevelCommandHandler
    {
        private readonly IResultLevelRepository _resultLevelRepository;

        public GetResultLevelCommandHandler(IResultLevelRepository resultLevelRepository)
        {
            this._resultLevelRepository = resultLevelRepository;
        }

        public async Task<ResponseEntity<ResultLevelEntity>> HandleAsync(string userId)
        {
            return await this._resultLevelRepository.GetAllUser(userId: userId);
        }

    }
}

