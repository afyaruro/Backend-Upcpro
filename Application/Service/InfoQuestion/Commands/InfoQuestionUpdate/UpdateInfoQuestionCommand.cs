using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionUpdate
{
    public class UpdateInfoQuestionCommand
    {
        public string Contexto { get; set; }
        public string Fuente { get; set; }
        public string TypeQuestion { get; set; }
        public string Id { get; set; }


    }
}