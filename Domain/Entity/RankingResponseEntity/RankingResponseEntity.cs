using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entity.RankingResponseEntity
{
    public class RankingResponseEntity<T>
    {
        public List<T> Top { get; set; } = new List<T>();
        public int CurrentUserPosition { get; set; }
    }
}