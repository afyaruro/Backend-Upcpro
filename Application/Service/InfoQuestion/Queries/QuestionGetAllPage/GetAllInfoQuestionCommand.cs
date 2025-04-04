using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionGetAllPage
{
    public class GetAllPageInfoQuestionOutputCommand
    {
        public string Contexto { get; set; }
        public string Fuente { get; set; }
        public string TypeQuestion { get; set; }
        public string Id { get; set; }

    }

    public class GetAllPageInfoQuestionInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public GetAllPageInfoQuestionInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}