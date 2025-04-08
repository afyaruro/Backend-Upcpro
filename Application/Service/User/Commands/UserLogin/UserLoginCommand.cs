using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Commands.UserLogin
{
    public class UserLoginCommand
    {
        public string Mail { get; set; }
        public string Password { get; set; }


        public UserLoginCommand(string mail, string password)
        {
            this.Mail = mail;
            this.Password = password;
        }
    }
}