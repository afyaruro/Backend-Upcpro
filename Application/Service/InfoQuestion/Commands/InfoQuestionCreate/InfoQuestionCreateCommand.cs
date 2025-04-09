

namespace Application.Service.InfoQuestion.Commands.InfoQuestionCreate
{
    public class InfoQuestionCreateInputCommand
    {
        public string Context{ get; set; }
        public string Image { get; set; }
        public string IdCompetence { get; set; }

        public InfoQuestionCreateInputCommand(string context, string image, string idCompetence)
        {
            this.Context = context;
            this.Image = image;
            this.IdCompetence = idCompetence;
        }
        
    }

    public class InfoQuestionCreateOutputCommand
    {
        public string Context { get; set; }
        public string Image { get; set; }
        public string Id { get; set; }
        public string IdCompetence { get; set; }


        public InfoQuestionCreateOutputCommand(string context, string image, string id, string idCompetence)
        {
            this.Context = context;
            this.Image = image;
            this.Id = id;
            this.IdCompetence = idCompetence;
        }
       
    }
}