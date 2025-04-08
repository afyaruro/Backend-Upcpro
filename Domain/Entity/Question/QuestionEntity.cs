
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
        [BsonElement("type")]
        public string OptionType { get; set; }
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
        [BsonElement("typeQuestion")]
        public string TypeQuestion { get; set; }

        [BsonIgnore]
        public InfoQuestionEntity? InfoQuestion { get; set; }

        public QuestionEntity(
            string enunciated,
            string feedback,
            string optionType,
            string optionA,
            string optionB,
            string optionC,
            string optionD,
            int correctAnswer,
            string idInfoQuestion,
            string typeQuestion)
        {
            Enunciated = enunciated;
            Feedback = feedback;
            OptionType = optionType;
            OptionA = optionA;
            OptionB = optionB;
            OptionC = optionC;
            OptionD = optionD;
            CorrectAnswer = correctAnswer;
            IdInfoQuestion = idInfoQuestion;
            TypeQuestion = typeQuestion;
            DateUpdate = DateTime.Now;
        }


    }
}