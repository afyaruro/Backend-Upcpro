
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
        

        public InfoQuestionEntity(string context, string image)
        {
            this.Context = context;
            this.Image = image;
            this.DateUpdate = DateTime.Now;
        }
      
    }
}