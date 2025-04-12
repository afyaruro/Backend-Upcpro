using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Simulacro.Commands.SimulacroGet
{
    public class SimulacroGetInputCommand
    {
        public DateTime FechaActual { get; set; }

        public SimulacroGetInputCommand(DateTime fechaActual)
        {
            FechaActual = fechaActual;
        }
    }
}