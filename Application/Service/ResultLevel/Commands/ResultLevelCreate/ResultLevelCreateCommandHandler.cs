
using Application.Common.Exceptions;
using Domain.Entity.Level;
using Domain.Port.Competence;
using Domain.Port.Level;
using FluentValidation;

namespace Application.Service.ResultLevel.Commands.ResultLevelCreate
{
    public class ResultLevelCreateCommandHandler
    {
        private readonly IResultLevelRepository _resultLevelRepository;
        private readonly ICompetenceRepository _competenceRepository;


        public ResultLevelCreateCommandHandler(IResultLevelRepository resultLevelRepository, ICompetenceRepository competenceRepository)
        {
            this._resultLevelRepository = resultLevelRepository;
            this._competenceRepository = competenceRepository;
        }

        public async Task<bool> HandleAsync(string userId)
        {

            var competences = await this._competenceRepository.GetAll(page: 0, size: 0);

            foreach (var competence in competences.listEntity!)
            {

                var exist = await this._resultLevelRepository.Exist(userId: userId, idCompetence: competence.Id);
                if(!exist)
                {
                    var resultLevel = new ResultLevelEntity
                    {
                        UserId = userId,
                        IdCompetence = competence.Id,
                        Score = 0,
                        PassedLevels = new List<string>()
                    };

                    await this._resultLevelRepository.Add(resultLevel);
                }
            }

            return true;

        }
    }
}

