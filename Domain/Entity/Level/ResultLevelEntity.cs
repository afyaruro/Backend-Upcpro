

using Domain.Base.BaseEntity;

namespace Domain.Entity.Level
{
    public class ResultLevelEntity:BaseEntity
    {
        public string UserId {get; set;}
        public List<string> PassedLevels {get; set;}
        

        public ResultLevelEntity()
        {
            PassedLevels = new List<string>();
            DateUpdate = DateTime.Now;
        }
        
        public ResultLevelEntity(string userId, List<string> passedLevels)
        {
            UserId = userId;
            PassedLevels = passedLevels;
            DateUpdate = DateTime.Now;
        }
    }
}