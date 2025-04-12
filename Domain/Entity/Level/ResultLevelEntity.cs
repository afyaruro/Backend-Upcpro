

using Domain.Base.BaseEntity;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.Level
{
    public class ResultLevelEntity:BaseEntity
    {
        [BsonElement("userId")]
        public string UserId {get; set;}
        [BsonElement("passedLevels")]
        public List<string> PassedLevels {get; set;}
        [BsonElement("score")]
        public double Score {get; set;}
        [BsonElement("idCompetence")]
        public string IdCompetence {get; set;}
        

        public ResultLevelEntity()
        {
            PassedLevels = new List<string>();
            DateUpdate = DateTime.Now;
        }
        
        public ResultLevelEntity(string userId, List<string> passedLevels, double score, string idCompetence)
        {
            UserId = userId;
            PassedLevels = passedLevels;
            Score = score;
            IdCompetence = idCompetence;
            DateUpdate = DateTime.Now;
        }
        
    }
}