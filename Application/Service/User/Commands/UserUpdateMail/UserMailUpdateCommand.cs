using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserMailUpdateInputCommand
    {
        public string Mail { get; set; }


        public UserMailUpdateInputCommand(string mail)
        {
            this.Mail = mail;
        }
    }

    public class UserMailUpdateForAdminInputCommand : UserMailUpdateInputCommand
    {
        public string Id { get; set; }

        public UserMailUpdateForAdminInputCommand(string mail)
      : base(mail)
        {
        }
    }
}