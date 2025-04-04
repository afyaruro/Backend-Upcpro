using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UpdateUserMailCommand
    {
        public string Mail { get; set; }


        public UpdateUserMailCommand(string mail)
        {
            this.Mail = mail;
        }
    }

    public class UpdateUserMailForAdminCommand : UpdateUserMailCommand
    {
        public string Id { get; set; }

        public UpdateUserMailForAdminCommand(string mail)
      : base(mail)
        {
        }
    }
}