

namespace Application.Service.Question.Commands.QuestionDelete
{
    public class QuestionDeleteInputCommand
    {
        public string Id { get; set; }

        public QuestionDeleteInputCommand(string id)
        {
            this.Id = id;
        }
    }
}