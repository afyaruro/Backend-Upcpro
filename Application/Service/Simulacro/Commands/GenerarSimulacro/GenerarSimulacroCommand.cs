using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Simulacro.Commands.GenerarSimulacro
{
    public class GenerarSimulacroInputCommand
    {
        public string Id { get; set; }

        public GenerarSimulacroInputCommand(string id)
        {
            this.Id = id;
        }
    }
}