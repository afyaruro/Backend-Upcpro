

namespace Application.Service.Competence.Commands.CompetenceCreate
{
    public class CompetenceCreateInputCommand
    {
        public string Name { get; set; }

        public CompetenceCreateInputCommand(string name)
        {
            this.Name = name;
        }
    }

    public class CompetenceOutputCreateCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public CompetenceOutputCreateCommand(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
    }

}