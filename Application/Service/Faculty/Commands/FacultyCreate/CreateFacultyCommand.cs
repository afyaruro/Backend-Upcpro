

namespace Application.Service.Faculty.Commands.FacultyCreate
{
    public class CreateInputFacultyCommand
    {
        public string Name { get; set; }

        public CreateInputFacultyCommand(string name)
        {
            this.Name = name;
        }
    }

    public class CreateOutputFacultyCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public CreateOutputFacultyCommand(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}