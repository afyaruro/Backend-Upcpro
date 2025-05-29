

namespace Application.Service.EspacioFisico.Commands.EspacioFisicoGetAllPage
{
   

    public class EspacioFisicoGetAllPageSyncInputCommand
    {
        public DateTime LateDateSync { get; set; }

        public EspacioFisicoGetAllPageSyncInputCommand(DateTime lateDateSync )
        {
            this.LateDateSync = lateDateSync;
        }
    }
}