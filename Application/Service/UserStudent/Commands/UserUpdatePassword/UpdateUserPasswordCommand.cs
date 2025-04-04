using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UpdateUserPasswordCommand
    {
        public string Password { get; set; }


        public UpdateUserPasswordCommand(string password)
        {
            this.Password = password;
        }
    }

    public class UpdateUserPasswordForAdminCommand : UpdateUserPasswordCommand
    {
        public string Id { get; set; }



        public UpdateUserPasswordForAdminCommand(string password)
       : base(password)
        {
        }
    }
}