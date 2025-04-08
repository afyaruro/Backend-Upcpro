using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Level.Commands.LevelUpdate
{
    public class LevelUpdateInputCommand
    {
         public string Id { get; set; }
        public int Level { get; set; }
        public int Dificulty { get; set; }
        public int Reward { get; set; }
        public int NumQuestion { get; set; }

        public LevelUpdateInputCommand()
        {
        }

        public LevelUpdateInputCommand(string id, int level, int dificulty, int numQuestion,
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