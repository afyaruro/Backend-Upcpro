using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Base.BaseEntity;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.Question
{
    public class InfoQuestionEntity : BaseEntity
    {
        [BsonElement("contexto")]
        public string Contexto { get; set; }
        [BsonElement("fuente")]
        public string Fuente { get; set; }
        [BsonElement("typeQuestion")]
        public string TypeQuestion { get; set; }

    }
}