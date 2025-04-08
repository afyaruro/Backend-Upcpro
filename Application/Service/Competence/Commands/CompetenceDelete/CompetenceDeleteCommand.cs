

namespace Application.Service.Competence.Commands.CompetenceDelete
{
    public class CompetenceDeleteInputCommand
    {
        public string Id { get; set; }

        public CompetenceDeleteInputCommand(string id)
        {
            this.Id = id;
        }
    }
}