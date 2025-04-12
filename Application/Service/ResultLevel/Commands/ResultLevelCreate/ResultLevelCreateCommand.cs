

namespace Application.Service.ResultLevel.Commands.ResultLevelCreate
{
    public class ResultLevelCreateInputCommand
    {
        public string UserId { get; set; }

        public ResultLevelCreateInputCommand()
        {
        }

        public ResultLevelCreateInputCommand(string userId, List<string> passedLevels, double score, string idCompetence)
        {
            UserId = userId;
        }

    }

  }