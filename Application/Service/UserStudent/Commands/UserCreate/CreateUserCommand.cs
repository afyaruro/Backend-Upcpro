using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity.Program;

namespace Application.Service.User.Commands.UserCreate
{
    public class CreateInputUserCommand
    {
        public string Mail { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string TypeIdentification { get; set; }
        public string Gender { get; set; }
        public string IdProgram { get; set; }

        public CreateInputUserCommand()
        {
        }
    }

    public class CreateOutputUserCommand
    {
        public string Id { get; set; }
        public string Mail { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string TypeIdentification { get; set; }
        public string Gender { get; set; }
        public string TypeUser { get; set; }
        public ProgramEntity Program { get; set; }

        public CreateOutputUserCommand()
        {
        }
    }
}