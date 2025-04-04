
using Domain.Base.BaseEntity;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.Question
{
    public class QuestionEntity : BaseEntity
    {
        [BsonElement("enunciated")]
        public string Enunciated { get; set; }
        [BsonElement("feedback")]
        public string Feedback { get; set; }
        [BsonElement("difficulty")]
        public string Difficulty { get; set; }
        [BsonElement("type")]
        public string OptionType { get; set; }  //image or text
        [BsonElement("a")]
        public string OptionA { get; set; }
        [BsonElement("b")]
        public string OptionB { get; set; }
        [BsonElement("c")]
        public string OptionC { get; set; }
        [BsonElement("d")]
        public string OptionD { get; set; }
        [BsonElement("correctAnswer")]
        public int CorrectAnswer { get; set; }
        [BsonElement("idInfoQuestion")]
        public string IdInfoQuestion { get; set; }
        [BsonIgnore]
        public InfoQuestionEntity InfoQuestion { get; set; }



    }
}