using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionDelete
{
    public class DeleteInfoQuestionCommand
    {
        public string Id { get; set; }

        public DeleteInfoQuestionCommand(string id)
        {
            this.Id = id;
        }
    }
}