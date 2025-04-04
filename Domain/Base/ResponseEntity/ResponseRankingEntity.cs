using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity.Score;

namespace Domain.Base.ResponseEntity
{
    public class ResponseRankingEntity : ResponseEntity<ScoreEntity>
    {
        public double score { get; set; }
        public int position { get; set; }
        public ResponseRankingEntity(string message, ScoreEntity entity) : base(message, entity)
        {
        }

        public ResponseRankingEntity(string message, List<ScoreEntity> list) : base(message, list)
        {
        }

        public ResponseRankingEntity(string message, bool isError) : base(message, isError)
        {
        }
    }
}