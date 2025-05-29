

namespace Application.Service.EspacioFisico.Commands.EspacioFisicoGetAllPage
{
    public class EspacioFisicoGetAllPageOutputCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public DateTime DateUpdate { get; set; }
        public EspacioFisicoGetAllPageOutputCommand(string name, string id, DateTime dateUpdate)
        {
            this.Name = name;
            this.Id = id;
            this.DateUpdate = dateUpdate;
        }

    }

    public class EspacioFisicoGetAllPageInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public EspacioFisicoGetAllPageInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}