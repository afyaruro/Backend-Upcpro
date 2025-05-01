using Domain.Entity;
using Domain.Entity.RankingResponseEntity;
using Domain.Entity.Simulacros;

namespace Domain.Port.Certificado
{
    public interface ICertificadoSimulacroRepository
    {
        Task<bool> CrearAsync(SimulacroResultEntity simulacro);
        Task<bool> ExistByUser(string idUser, string idSimulacro);
        Task<List<SimulacroResultEntity>> GetAll(string idUser);
        Task<(RankingResponseEntity<SimulacroResultEntity>, List<UserEntity>)> GetRankingByScore(string userId, string idSimulacro);

        //falta que el administrador pueda visualizar todos los resultados de un simulacro y tambien todos los simulacros

    }
}