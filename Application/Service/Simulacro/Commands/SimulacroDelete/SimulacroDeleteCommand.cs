

namespace Application.Service.Simulacro.Commands.SimulacroDelete
{
    public class SimulacroDeleteInputCommand
    {
        public string Id { get; set; }

        public SimulacroDeleteInputCommand(string id)
        {
            this.Id = id;
        }
    }
}