

namespace Application.Service.InfoQuestion.Commands.InfoQuestionCreate
{
    public class InfoQuestionCreateInputCommand
    {
        public string Context{ get; set; }
        public string Image { get; set; }

        public InfoQuestionCreateInputCommand(string context, string image)
        {
            this.Context = context;
            this.Image = image;
        }
        
    }

    public class InfoQuestionCreateOutputCommand
    {
        public string Context { get; set; }
        public string Image { get; set; }
        public string Id { get; set; }


        public InfoQuestionCreateOutputCommand(string context, string image, string id)
        {
            this.Context = context;
            this.Image = image;
            this.Id = id;
        }
       
    }
}