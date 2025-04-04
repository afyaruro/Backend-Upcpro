using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Competence.Commands.CompetenceUpdate
{
    public class UpdateCompetenceCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public UpdateCompetenceCommand(string name, string id)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}