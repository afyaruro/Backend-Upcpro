using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Competence.Commands.CompetenceDelete
{
    public class DeleteCompetenceCommand
    {
        public string Id { get; set; }

        public DeleteCompetenceCommand(string id)
        {
            this.Id = id;
        }
    }
}