using Application.Service.Faculty.Commands.FacultyCreate;

namespace Application.Service.Simulacro.Commands.SimulacroCreate

{
    public class SimulacroCreateInputCommand
    {

        public string Type { get; set; }
        public int Duracion { get; set; }
        public int NumeroPreguntas { get; set; }
        public DateTime FechaLimite { get; set; }

        public SimulacroCreateInputCommand(int duracion, int numeroPreguntas, DateTime fechaLimite, string type)
        {
            this.Duracion = duracion;
            this.NumeroPreguntas = numeroPreguntas;
            this.FechaLimite = fechaLimite;
            this.Type = type;
        }
    }

    public class SimulacroCreateOutputCommand
    {
        public int Duracion { get; set; }
        public int NumeroPreguntas { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Id { get; set; }
        public SimulacroCreateOutputCommand(int duracion, int numeroPreguntas, DateTime fechaLimite, string id)
        {
            this.Duracion = duracion;
            this.NumeroPreguntas = numeroPreguntas;
            this.FechaLimite = fechaLimite;
            this.Id = id;
        }
    }
}