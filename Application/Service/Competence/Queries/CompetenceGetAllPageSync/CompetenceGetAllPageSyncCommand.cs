

namespace Application.Service.Competence.Commands.CompetenceGetAllPage
{
   

    public class CompetenceGetAllPageSyncInputCommand
    {
        public DateTime LateDateSync { get; set; }

        public CompetenceGetAllPageSyncInputCommand(DateTime lateDateSync )
        {
            this.LateDateSync = lateDateSync;
        }
    }
}