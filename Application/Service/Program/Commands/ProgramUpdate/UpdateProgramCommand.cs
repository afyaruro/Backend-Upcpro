using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Program.Commands.ProgramUpdate
{
    public class UpdateProgramCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IdFaculty { get; set; }

        public UpdateProgramCommand(string name, string id, string idFaculty)
        {
            this.Id = id;
            this.Name = name;
            this.IdFaculty = idFaculty;
        }
    }
}