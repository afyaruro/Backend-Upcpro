

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class CompetenceGetAllPageOutputCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public CompetenceGetAllPageOutputCommand(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
    }

    public class CompetenceGetAllPageInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public CompetenceGetAllPageInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}