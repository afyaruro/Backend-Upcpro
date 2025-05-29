using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserPasswordUpdateInputCommand
    {
        public string Password { get; set; }
        public string PasswordActual { get; set; }



        public UserPasswordUpdateInputCommand(string password, string passwordActual)
        {
            this.Password = password;
            this.PasswordActual = passwordActual;
        }
    }





    // public class UserPasswordForAdminUpdateInputCommand 
    // {
    //     public string Id { get; set; }
    //     public string Password { get; set; }


    //     public UserPasswordForAdminUpdateInputCommand(string password)

    //     {
    //         this.Password = password;
    //     }
    // }
}