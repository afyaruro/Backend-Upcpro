using Domain.Entity.Question;

namespace Application.Service.Question.Commands.QuestionCreate
{
    public class QuestionCreateInputCommand
    {
        public string Enunciated { get; set; }
        public string Feedback { get; set; }
        public string OptionType { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public int CorrectAnswer { get; set; }
        public string IdInfoQuestion { get; set; }
        public string TypeQuestion { get; set; }
        public string IdCompetence { get; set; }

        public QuestionCreateInputCommand() { }

        public QuestionCreateInputCommand(
            string enunciated,
            string feedback,
            string optionType,
            string optionA,
            string optionB,
            string optionC,
            string optionD,
            int correctAnswer,
            string idInfoQuestion,
            string typeQuestion,
            string idCompetence)
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
            IdCompetence = idCompetence;
        }
    }

    public class QuestionCreateOutputCommand
    {
        public string Id { get; set; }
        public string Enunciated { get; set; }
        public string Feedback { get; set; }
        public string OptionType { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public int CorrectAnswer { get; set; }
        public string TypeQuestion { get; set; }
        public string IdCompetence { get; set; }
        public string IdInfoQuestion { get; set; }

        public QuestionCreateOutputCommand() { }

        public QuestionCreateOutputCommand(
            string id,
            string enunciated,
            string feedback,
            string optionType,
            string optionA,
            string optionB,
            string optionC,
            string optionD,
            int correctAnswer,
            string typeQuestion,
            string idCompetence,
            string idInfoQuestion)
        {
            Id = id;
            Enunciated = enunciated;
            Feedback = feedback;
            OptionType = optionType;
            OptionA = optionA;
            OptionB = optionB;
            OptionC = optionC;
            OptionD = optionD;
            CorrectAnswer = correctAnswer;
            TypeQuestion = typeQuestion;
            IdCompetence = idCompetence;
            IdInfoQuestion = idInfoQuestion;
        }
    }
}
