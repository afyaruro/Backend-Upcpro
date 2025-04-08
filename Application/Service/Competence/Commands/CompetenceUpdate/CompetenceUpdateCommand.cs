using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Competence.Commands.CompetenceUpdate
{
    public class CompetenceUpdateInputCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public CompetenceUpdateInputCommand(string name, string id)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}