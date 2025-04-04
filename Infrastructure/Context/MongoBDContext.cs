using Infrastructure.Config;
using MongoDB.Driver;

namespace Infrastructure.Context.MongoDB
{
    public class MongoDBContext
    {
        public readonly MongoClient _db;
        public readonly IMongoDatabase _dbs;

        private readonly string _connectionString;

        public MongoDBContext(IConfigMongoDB _conf)
        {
            _connectionString = _conf.Server;
            _db = new MongoClient(_connectionString);
            _dbs = _db.GetDatabase(_conf.Database);
        }
    }
}