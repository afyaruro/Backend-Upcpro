
using Domain.Base.BaseEntity;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.Facultad
{
    public class FacultyEntity : BaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        public FacultyEntity()
        {

        }

        public FacultyEntity(string name)
        {

            this.Name = name;
            this.DateUpdate = DateTime.Now;
        }
    }
}