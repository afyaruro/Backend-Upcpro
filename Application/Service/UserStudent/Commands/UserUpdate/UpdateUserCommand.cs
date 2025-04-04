using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UpdateUserCommand
    {
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string TypeIdentification { get; set; }
        public string Gender { get; set; }
        public string IdProgram { get; set; }

        public UpdateUserCommand()
        {
        }
    }

    public class UpdateUserForAdminCommand : UpdateUserCommand
    {
        public string Id { get; set; }

        public UpdateUserForAdminCommand()
        {
        }
    }
}