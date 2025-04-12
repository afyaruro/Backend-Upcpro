

using Application.Service.ResultLevel.Commands.GetRanking;
using Application.Service.ResultLevel.Commands.GetResultLevel;
using Application.Service.ResultLevel.Commands.ResultLevelCreate;
using Application.Service.ResultLevel.Commands.ResultLevelUpdate;
using Domain.Base.ResponseEntity;
using Domain.Entity.Level;
using Domain.Port.Competence;
using Domain.Port.Level;

namespace Application.Service.Level
{
    public class ResultLevelService
    {
        private readonly IResultLevelRepository _repository;
        private readonly ICompetenceRepository _competenceRepository;
        public ResultLevelService(IResultLevelRepository repository, ICompetenceRepository competenceRepository)
        {
            _repository = repository;
            _competenceRepository = competenceRepository;
        }
        public async Task<bool> Create(string userId)
        {
            var _create = new ResultLevelCreateCommandHandler(_repository, _competenceRepository);
            return await _create.HandleAsync(userId);
        }

        public async Task<ResponseEntity<ResultLevelEntity>> GetAllUser(string userId)
        {
            var _getAll = new GetResultLevelCommandHandler(_repository);
            return await _getAll.HandleAsync(userId);
        }

        public async Task<bool> Update(ResultLevelUpdateInputCommand command, string userId)
        {
            var _update = new ResultLevelUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command, userId);
        }

        public async Task<(List<ResultLevelEntity>, int)> Ranking(RankingInputCommand command, string userId)
        {
            var ranking = new RankingCommandHandler(_repository);
            return await ranking.HandleAsync(command, userId);
        }


    }
}