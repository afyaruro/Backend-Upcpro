using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserPuntajeUpdateInputCommand
    {
        public int Puntaje { get; set; }

        public UserPuntajeUpdateInputCommand(int puntaje)
        {
            this.Puntaje = puntaje;
        }
    }


}