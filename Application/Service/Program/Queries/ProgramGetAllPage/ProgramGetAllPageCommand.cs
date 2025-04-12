

using Application.Service.Faculty.Commands.FacultyGetAllPage;

namespace Application.Service.Program.Commands.ProgramGetAllPage
{
    public class ProgramGetAllPageOutputCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public FacultyGetAllPageOutputCommand Faculty { get; set; }
        public DateTime DateUpdate { get; set; }

        public ProgramGetAllPageOutputCommand(string name, string id, FacultyGetAllPageOutputCommand faculty, DateTime dateUpdate)
        {
            this.Name = name;
            this.Id = id;
            this.Faculty = faculty;
            this.DateUpdate = dateUpdate;
        }
        
    }

    public class ProgramGetAllPageInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public ProgramGetAllPageInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}