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
        Task<ResponseEntity<CompetenceEntity>> GetAll(int page, int size);
        Task<ResponseEntity<CompetenceEntity>> GetAll(DateTime lastSyncDate);

    }
}