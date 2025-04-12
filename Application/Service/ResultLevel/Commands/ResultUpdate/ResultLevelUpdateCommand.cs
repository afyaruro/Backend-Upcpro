

namespace Application.Service.ResultLevel.Commands.ResultLevelUpdate
{
    public class ResultLevelUpdateInputCommand
    {
        public string LevelId { get; set; }
        public double Score { get; set; }
        public string IdCompetence { get; set; }

        public ResultLevelUpdateInputCommand()
        {
        }

        public ResultLevelUpdateInputCommand( string levelId, double score, string idCompetence)
        {
            LevelId = levelId;
            Score = score;
            IdCompetence = idCompetence;
        }


    }
}