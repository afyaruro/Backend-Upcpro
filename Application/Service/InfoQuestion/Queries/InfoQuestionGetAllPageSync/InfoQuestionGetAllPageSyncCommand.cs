

namespace Application.Service.InfoQuestion.Commands.InfoQuestionGetAllPage
{
   

    public class InfoQuestionGetAllPageSyncInputCommand
    {
        public DateTime LateDateSync { get; set; }

        public InfoQuestionGetAllPageSyncInputCommand(DateTime lateDateSync )
        {
            this.LateDateSync = lateDateSync;
        }
    }
}