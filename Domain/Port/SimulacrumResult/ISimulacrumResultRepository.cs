using Domain.Entity;
using Domain.Entity.RankingResponseEntity;
using Domain.Entity.Simulacros;

namespace Domain.Port.SimulacrumResult
{
    public interface ISimulacrumResultRepository
    {
        Task<bool> CrearAsync(SimulacrumResultEntity simulacro);
        Task<bool> ExistByUser(string idUser, string idSimulacro);
        Task<List<SimulacrumResultEntity>> GetAll(string idUser);
        

    }
}