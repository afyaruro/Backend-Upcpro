

namespace Application.Service.Faculty.Commands.FacultyCreate
{
    public class FacultyCreateInputCommand
    {
        public string Name { get; set; }

        public FacultyCreateInputCommand(string name)
        {
            this.Name = name;
        }
    }

    public class FacultyCreateOutputCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public FacultyCreateOutputCommand(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
    }

   
}