using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Faculty.Commands.FacultyUpdate
{
    public class UpdateFacultyCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public UpdateFacultyCommand(string name, string id)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}