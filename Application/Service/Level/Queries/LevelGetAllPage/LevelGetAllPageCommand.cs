

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class LevelGetAllPageOutputCommand
    {
        public string Id { get; set; }
        public int Level { get; set; }
        public int Dificulty { get; set; }
        public int Reward { get; set; }
        public int NumQuestion { get; set; }


        public LevelGetAllPageOutputCommand()
        {
        }

        public LevelGetAllPageOutputCommand(string id, int level, int dificulty, int numQuestion,
            int reward)
        {
            Id = id;
            Level = level;
            Dificulty = dificulty;
            Reward = reward;
            NumQuestion = numQuestion;
        }
    }

    public class LevelGetAllPageInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public LevelGetAllPageInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}