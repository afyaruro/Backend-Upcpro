

using Domain.Entity.Question;

namespace Application.Service.Question.Commands.QuestionGetAllPage
{
    public class QuestionGetAllPageOutputCommand
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
        public InfoQuestionEntity? InfoQuestion { get; set; }

         public QuestionGetAllPageOutputCommand() { }

    public QuestionGetAllPageOutputCommand(
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
        InfoQuestionEntity? infoQuestion)
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
        InfoQuestion = infoQuestion;
    }

    }

    public class QuestionGetAllPageInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public QuestionGetAllPageInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}