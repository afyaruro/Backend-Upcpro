

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class LevelGetAllPageOutputCommand
    {
        public string Id { get; set; }
        public int Level { get; set; }
        public int Dificulty { get; set; }
        public int Reward { get; set; }
        public List<string> Questions { get; set; }
        public string IdCompetence { get; set; }
        public DateTime DateTime { get; set; }


        public LevelGetAllPageOutputCommand()
        {
        }

        public LevelGetAllPageOutputCommand(string id, int level, int dificulty, string idCompetence, List<string> questions, int reward, DateTime dateTime)
        {
            Id = id;
            Level = level;
            Dificulty = dificulty;
            Questions = questions;
            IdCompetence = idCompetence;
            Reward = reward;
            DateTime = dateTime;
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