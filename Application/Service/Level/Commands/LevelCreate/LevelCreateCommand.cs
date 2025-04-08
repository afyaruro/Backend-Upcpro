namespace Application.Service.Level.Commands.LevelCreate
{
    public class LevelCreateInputCommand
    {
        public int Level { get; set; }
        public int Dificulty { get; set; }
        public int Reward { get; set; }
        public int NumQuestion { get; set; }

        public LevelCreateInputCommand()
        {
        }

        public LevelCreateInputCommand(int level, int dificulty, int reward,
            int numQuestion)
        {
            Level = level;
            Dificulty = dificulty;
            Reward = reward;
            NumQuestion = numQuestion;
        }
    }

    public class LevelOutputCreateCommand
    {
        public string Id { get; set; }
        public int Level { get; set; }
        public int Dificulty { get; set; }
        public int Reward { get; set; }
        public int NumQuestion { get; set; }


        public LevelOutputCreateCommand()
        {
        }

        public LevelOutputCreateCommand(string id, int level, int dificulty, int numQuestion,
            int reward)
        {
            Id = id;
            Level = level;
            Dificulty = dificulty;
            Reward = reward;
            NumQuestion = numQuestion;
        }
    }
}