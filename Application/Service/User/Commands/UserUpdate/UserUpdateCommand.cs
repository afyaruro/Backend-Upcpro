using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserUpdateInputCommand
    {
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string TypeIdentification { get; set; }
        public string Gender { get; set; }
        public string IdProgram { get; set; }

        public UserUpdateInputCommand()
        {
        }
    }

    public class UserForAdminUpdateInputCommand : UserUpdateInputCommand
    {
        public string Id { get; set; }

        public UserForAdminUpdateInputCommand()
        {
        }
    }
}