

using Infrastructure.Config;

namespace Infrastructure.Config
{
    public class ConfigMongoDB : IConfigMongoDB
    {
        public string Server { get; set; }
        public string Database { get; set; }
    }
}