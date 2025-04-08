

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class FacultyGetAllPageOutputCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public FacultyGetAllPageOutputCommand(string name, string id)
        {
            this.Name = name;
            this.Id = id;
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