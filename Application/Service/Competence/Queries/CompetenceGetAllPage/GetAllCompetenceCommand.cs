using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Competence.Commands.CompetenceGetAllPage
{
    public class GetAllPageCompetenceOutputCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public GetAllPageCompetenceOutputCommand(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
    }

    public class GetAllPageCompetenceInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public GetAllPageCompetenceInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}