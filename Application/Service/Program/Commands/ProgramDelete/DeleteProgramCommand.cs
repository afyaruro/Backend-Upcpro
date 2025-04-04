

namespace Application.Service.Program.Commands.ProgramDelete
{
    public class DeleteProgramCommand
    {
        public string Id { get; set; }

        public DeleteProgramCommand(string id)
        {
            this.Id = id;
        }
    }
}