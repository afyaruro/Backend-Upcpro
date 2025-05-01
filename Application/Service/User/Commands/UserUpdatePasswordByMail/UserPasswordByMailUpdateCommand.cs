using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Commands.UserUpdate
{

    public class UserPasswordByMailUpdateInputCommand
    {
        public string Password { get; set; }
        public string Mail { get; set; }



        public UserPasswordByMailUpdateInputCommand(string password, string mail)
        {
            this.Password = password;
            this.Mail = mail;
        }
    }


}