

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class GetAllPageFacultyOutputCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public GetAllPageFacultyOutputCommand(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
    }

    public class GetAllPageFacultyInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public GetAllPageFacultyInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}