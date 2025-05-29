

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class FacultyGetAllPageOutputCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public DateTime DateUpdate { get; set; }
        public string EspacioFisicoId { get; set; }
        public FacultyGetAllPageOutputCommand(string name, string id, DateTime dateUpdate, string espacioFisicoId)
        {
            this.Name = name;
            this.Id = id;
            this.DateUpdate = dateUpdate;
            this.EspacioFisicoId = espacioFisicoId;
        }

    }

    public class FacultyGetAllPageInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public FacultyGetAllPageInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}