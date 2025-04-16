

namespace Application.Service.Program.Commands.ProgramGetAllPage
{
   

    public class ProgramGetAllPageSyncInputCommand
    {
        public DateTime LateDateSync { get; set; }

        public ProgramGetAllPageSyncInputCommand(DateTime lateDateSync )
        {
            this.LateDateSync = lateDateSync;
        }
    }
}