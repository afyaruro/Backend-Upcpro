
using Domain.Base.BaseEntity;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.Simulacros
{
    public class SimulacroEntity : BaseEntity
    {
        [BsonElement("duracion")]
        public int Duracion { get; set; }
        [BsonElement("numeroPreguntas")]
        public int NumeroPreguntas { get; set; }
        [BsonElement("fechaLimite")]
        public DateTime FechaLimite { get; set; }
        
        public SimulacroEntity()
        {
            this.DateUpdate = DateTime.Now;
        }

        public SimulacroEntity(int duracion, int numeroPreguntas, DateTime fechaLimite)
        {
            Duracion = duracion;
            NumeroPreguntas = numeroPreguntas;
            FechaLimite = fechaLimite;
            this.DateUpdate = DateTime.Now;
        }


    }
}