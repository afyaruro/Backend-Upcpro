

namespace Application.Service.Faculty.Commands.FacultyDelete
{
    public class DeleteFacultyCommand
    {
        public string Id { get; set; }

        public DeleteFacultyCommand(string id)
        {
            this.Id = id;
        }
    }
}