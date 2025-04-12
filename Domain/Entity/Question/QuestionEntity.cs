
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
        [BsonElement("idCompetence")]
        public string IdCompetence { get; set; }


        public QuestionEntity(string enunciated, string feedback, string optionType, string optionA, string optionB, string optionC, string optionD, int correctAnswer, string idInfoQuestion, string typeQuestion, string idCompetence)
        {
            this.Enunciated = enunciated;
            this.Feedback = feedback;
            this.OptionType = optionType;
            this.OptionA = optionA;
            this.OptionB = optionB;
            this.OptionC = optionC;
            this.OptionD = optionD;
            this.CorrectAnswer = correctAnswer;
            this.IdInfoQuestion = idInfoQuestion;
            this.TypeQuestion = typeQuestion;
            this.IdCompetence = idCompetence;
            this.DateUpdate = DateTime.Now;
        }


    }
}