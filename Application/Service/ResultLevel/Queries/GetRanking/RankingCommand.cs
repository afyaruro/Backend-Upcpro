

namespace Application.Service.ResultLevel.Commands.GetRanking
{
    public class RankingOutputCommand
    {
        public string UserId { get; set; }
        public List<string> LevelsPassed { get; set; }
        public double Score { get; set; }
        public string IdCompetence { get; set; }



        public RankingOutputCommand()
        {
        }

        public RankingOutputCommand(string userId, List<string> levelsPassed, double score, string idCompetence)
        {
            UserId = userId;
            LevelsPassed = levelsPassed;
            Score = score;
            IdCompetence = idCompetence;
        }




    }

    public class RankingInputCommand
    {
        public string IdCompetence { get; set; }


        public RankingInputCommand( string idCompetence)
        {
            this.IdCompetence = idCompetence;
        }

    }
}