
namespace Application.Service.Question.Commands.QuestionUpdate
{
    public class QuestionUpdateInputCommand
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
        public string IdInfoQuestion { get; set; }
    }
}