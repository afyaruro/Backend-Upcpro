

using Domain.Entity.Simulacros;

namespace Domain.Port.Certificado
{
    public interface ICertificadoSimulacroRepository
    {
        Task<bool> CrearAsync(SimulacroResultEntity simulacro);
        Task<bool> ExistByUser(string idUser, string idSimulacro);
        Task<List<SimulacroResultEntity>> GetAll(string idUser);
    }
}