using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserPasswordUpdateInputCommand
    {
        public string Password { get; set; }


        public UserPasswordUpdateInputCommand(string password)
        {
            this.Password = password;
        }
    }

    public class UserPasswordForAdminUpdateInputCommand : UserPasswordUpdateInputCommand
    {
        public string Id { get; set; }



        public UserPasswordForAdminUpdateInputCommand(string password)
       : base(password)
        {
        }
    }
}