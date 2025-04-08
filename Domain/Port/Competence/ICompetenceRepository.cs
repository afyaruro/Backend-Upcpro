using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Base.ResponseEntity;
using Domain.Entity.Competence;

namespace Domain.Port.Competence
{
    public interface ICompetenceRepository
    {
        Task<bool> ExistById(string id);
        Task<CompetenceEntity> ExistByName(string name);
        Task<CompetenceEntity> Add(CompetenceEntity entity);
        Task<bool> Update(CompetenceEntity entity);
        Task<bool> Delete(string id);
        Task<ResponseEntity<CompetenceEntity>> GetAll(int page, int size);
    }
}