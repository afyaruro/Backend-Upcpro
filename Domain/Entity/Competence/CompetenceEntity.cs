
using Domain.Base.BaseEntity;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.Competence
{
    public class CompetenceEntity : BaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        public CompetenceEntity()
        {

        }

        public CompetenceEntity(string name)
        {

            this.Name = name;
            this.DateUpdate = DateTime.Now;
        }
    }
}

