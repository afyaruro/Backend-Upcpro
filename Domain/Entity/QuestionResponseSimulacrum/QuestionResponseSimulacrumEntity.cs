using Domain.Entity.Question;

namespace Domain.Entity.QuestionResponseSimulacrum
{
    public class QuestionResponseSimulacrumEntity : QuestionEntity
    {

        public int SelectedOption { get; set; }
        public bool IsResponse { get; set; }
        public InfoQuestionEntity? InfoQuestion { get; set; }

        public QuestionResponseSimulacrumEntity(string enunciated,
        string feedback, string optionType, string optionA, string optionB,
         string optionC, string optionD, int correctAnswer, string idInfoQuestion,
          string typeQuestion, string idCompetence, int selectedOption, bool isResponse, InfoQuestionEntity infoQuestion) :
          base(enunciated, feedback, optionType, optionA, optionB, optionC, optionD,
          correctAnswer, idInfoQuestion, typeQuestion, idCompetence)
        {
            this.InfoQuestion = infoQuestion;
            this.IsResponse = isResponse;
            this.SelectedOption = selectedOption;
        }
    }
}