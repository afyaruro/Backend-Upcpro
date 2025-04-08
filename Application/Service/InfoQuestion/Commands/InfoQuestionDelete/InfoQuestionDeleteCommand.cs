using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionDelete
{
    public class InfoQuestionDeleteInputCommand
    {
        public string Id { get; set; }

        public InfoQuestionDeleteInputCommand(string id)
        {
            this.Id = id;
        }
    }
}