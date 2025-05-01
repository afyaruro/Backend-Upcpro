
using Domain.Base.BaseEntity;
using Domain.Entity.Program;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity
{
    public class UserEntity : BaseEntity
    {
        [BsonElement("mail")]
        public string Mail { get; set; }
        [BsonElement("image")]
        public string Image { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("identification")]
        public string Identification { get; set; }
        [BsonElement("typeIdentification")]
        public string TypeIdentification { get; set; }
        [BsonElement("gender")]
        public string Gender { get; set; }
        [BsonElement("typeUser")]
        public string TypeUser { get; set; }
        [BsonElement("idProgram")]
        public string IdProgram { get; set; }
        [BsonElement("puntaje")]
        public int Puntaje { get; set; }

        [BsonIgnore]
        public ProgramEntity Program { get; set; }

        public UserEntity()
        {
            this.DateUpdate = DateTime.Now;
            this.Puntaje = 0;
        }

    }
}