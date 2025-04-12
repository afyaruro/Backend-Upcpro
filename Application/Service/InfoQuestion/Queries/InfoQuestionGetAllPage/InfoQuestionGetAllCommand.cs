using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionGetAllPage
{
    public class InfoQuestionGetAllPageOutputCommand
    {
        public string Context { get; set; }
        public string Image { get; set; }
        public string Id { get; set; }
        public DateTime dateUpdate { get; set; }

        public InfoQuestionGetAllPageOutputCommand(string id, string image, string context, DateTime dateUpdate){
            this.Id = id;
            this.Image = image;
            this.Context = context;
            this.dateUpdate = dateUpdate;
        }

    }

    public class InfoQuestionGetAllPageInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public InfoQuestionGetAllPageInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}