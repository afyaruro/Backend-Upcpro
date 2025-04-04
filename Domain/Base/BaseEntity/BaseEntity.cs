using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Base.BaseEntity
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}