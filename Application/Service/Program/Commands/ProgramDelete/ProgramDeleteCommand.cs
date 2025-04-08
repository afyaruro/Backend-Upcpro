

namespace Application.Service.Program.Commands.ProgramDelete
{
    public class ProgramDeleteInputCommand
    {
        public string Id { get; set; }

        public ProgramDeleteInputCommand(string id)
        {
            this.Id = id;
        }
    }
}