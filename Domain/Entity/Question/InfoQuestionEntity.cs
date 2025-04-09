
using Domain.Base.BaseEntity;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.Question
{
    public class InfoQuestionEntity : BaseEntity
    {
        [BsonElement("context")]
        public string Context { get; set; }
        [BsonElement("image")]
        public string Image { get; set; }
        [BsonElement("idCompetence")]
        public string IdCompetence { get; set; }

        public InfoQuestionEntity(string context, string image, string idCompetence)
        {
            this.Context = context;
            this.Image = image;
            this.IdCompetence = idCompetence;
            this.DateUpdate = DateTime.Now;
        }
      
    }
}