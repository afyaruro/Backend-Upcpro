

namespace Application.Service.InfoQuestion.Commands.InfoQuestionCreate
{
    public class CreateInputInfoQuestionCommand
    {
        public string Contexto { get; set; }
        public string Fuente { get; set; }
        public string TypeQuestion { get; set; }

        public CreateInputInfoQuestionCommand(string context, string fuente, string typeQuestion)
        {
            this.Contexto = context;
            this.Fuente = fuente;
            this.TypeQuestion = typeQuestion;
        }
    }

    public class CreateOutputInfoQuestionCommand
    {
        public string Contexto { get; set; }
        public string Fuente { get; set; }
        public string TypeQuestion { get; set; }
        public string Id { get; set; }


        public CreateOutputInfoQuestionCommand(string context, string fuente, string typeQuestion, string id)
        {
            this.Contexto = context;
            this.Fuente = fuente;
            this.TypeQuestion = typeQuestion;
            this.Id = id;
        }
    }
}