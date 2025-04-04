using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Competence.Commands.CompetenceCreate
{
    public class CreateInputCompetenceCommand
    {
        public string Name { get; set; }

        public CreateInputCompetenceCommand(string name)
        {
            this.Name = name;
        }
    }

    public class CreateOutputCompetenceCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public CreateOutputCompetenceCommand(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}