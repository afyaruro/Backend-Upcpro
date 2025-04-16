

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
   

    public class FacultyGetAllPageSyncInputCommand
    {
        public DateTime LateDateSync { get; set; }

        public FacultyGetAllPageSyncInputCommand(DateTime lateDateSync )
        {
            this.LateDateSync = lateDateSync;
        }
    }
}