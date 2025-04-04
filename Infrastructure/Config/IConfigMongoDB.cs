

namespace Infrastructure.Config
{
    public interface IConfigMongoDB
    {
        string Server { get; set; }
        string Database { get; set; }
    }
}