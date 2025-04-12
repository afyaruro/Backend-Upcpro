using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Simulacro.Commands.SimulacroUpdate
{
    public class SimulacroUpdateInputCommand
    {
        public int Duracion { get; set; }
        public int NumeroPreguntas { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Id { get; set; }

        public SimulacroUpdateInputCommand(int duracion, int numeroPreguntas, DateTime fechaLimite, string id)
        {
            this.Duracion = duracion;
            this.NumeroPreguntas = numeroPreguntas;
            this.FechaLimite = fechaLimite;
            this.Id = id;
        }
    }
}