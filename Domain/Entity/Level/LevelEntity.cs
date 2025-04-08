
using Domain.Base.BaseEntity;
using Domain.Entity.Question;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.Level
{
    public class LevelEntity : BaseEntity
    {
        [BsonElement("level")]
        public int Level { get; set; }
        [BsonElement("dificulty")]
        public int Dificulty { get; set; }
        [BsonElement("reward")]
        public int Reward { get; set; }
        
        [BsonElement("numQuestions")]

        public  int NumQuestion{ get; set; }



        public LevelEntity()
        {
            DateUpdate = DateTime.Now;
        }

        public LevelEntity(int level, int dificulty, int reward, int numQuestion)
        {
            Level = level;
            Dificulty = dificulty;
            Reward = reward;
            DateUpdate = DateTime.Now;
            NumQuestion = numQuestion!;
        }
    }
}