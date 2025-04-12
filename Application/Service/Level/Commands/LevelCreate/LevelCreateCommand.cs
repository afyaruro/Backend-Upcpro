namespace Application.Service.Level.Commands.LevelCreate
{
    public class LevelCreateInputCommand
    {
        public int Level { get; set; }
        public int Dificulty { get; set; }
        public int Reward { get; set; }
        public List<string> Questions { get; set; }
        public string IdCompetence { get; set; }

        public LevelCreateInputCommand()
        {
        }

        public LevelCreateInputCommand(int level, int dificulty, int reward,
            List<string> questions, string idCompetence)
        {
            Level = level;
            Dificulty = dificulty;
            Reward = reward;
            Questions = questions;
            IdCompetence = idCompetence;
        }
    }

    public class LevelOutputCreateCommand
    {
        public string Id { get; set; }
        public int Level { get; set; }
        public int Dificulty { get; set; }
        public int Reward { get; set; }
        public List<string> Questions { get; set; }
        public string IdCompetence { get; set; }


        public LevelOutputCreateCommand()
        {
        }

        public LevelOutputCreateCommand(string id, int level, int dificulty,
            int reward, List<string> questions, string idCompetence)
        {
            Id = id;
            Level = level;
            Dificulty = dificulty;
            Reward = reward;
            Questions = questions;
            IdCompetence = idCompetence;
        }
    }
}