

namespace Application.Service.Faculty.Commands.FacultyDelete
{
    public class FacultyDeleteInputCommand
    {
        public string Id { get; set; }

        public FacultyDeleteInputCommand(string id)
        {
            this.Id = id;
        }
    }
}