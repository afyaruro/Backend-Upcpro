

namespace Application.Service.Question.Commands.QuestionGetAllPage
{
   

    public class QuestionGetAllPageSyncInputCommand
    {
        public DateTime LateDateSync { get; set; }

        public QuestionGetAllPageSyncInputCommand(DateTime lateDateSync )
        {
            this.LateDateSync = lateDateSync;
        }
    }
}