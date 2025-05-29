
using Domain.Base.BaseEntity;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.EspacioFisico
{
    public class EspacioFisicoEntity : BaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        public EspacioFisicoEntity()
        {

        }

        public EspacioFisicoEntity(string name)
        {

            this.Name = name;
            this.DateUpdate = DateTime.Now;
        }
    }
}