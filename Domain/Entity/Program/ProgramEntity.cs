
using Domain.Base.BaseEntity;
using Domain.Entity.Facultad;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.Program
{
    public class ProgramEntity : BaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("idFaculty")]
        public string IdFaculty { get; set; }
        [BsonIgnore]
        public FacultyEntity Faculty { get; set; }


        public ProgramEntity()
        {

        }

        public ProgramEntity(string name, string idFaculty)
        {
            this.IdFaculty = idFaculty;
            this.Name = name;
            this.DateUpdate = DateTime.Now;
        }

    }
}