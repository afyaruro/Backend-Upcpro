

namespace Application.Service.Level.Commands.LevelDelete
{
    public class LevelDeleteInputCommand
    {
        public string Id { get; set; }

        public LevelDeleteInputCommand(string id)
        {
            this.Id = id;
        }
    }
}