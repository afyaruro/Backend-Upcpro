

using Application.Service.Faculty.Commands.FacultyGetAllPage;

namespace Application.Service.Program.Commands.ProgramGetAllPage
{
    public class GetAllPageProgramOutputCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public GetAllPageFacultyOutputCommand Faculty { get; set; }

        public GetAllPageProgramOutputCommand(string name, string id, GetAllPageFacultyOutputCommand faculty)
        {
            this.Name = name;
            this.Id = id;
            this.Faculty = faculty;
        }
    }

    public class GetAllPageProgramInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public GetAllPageProgramInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}