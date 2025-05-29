
using Domain.Base.ResponseEntity;
using Domain.Entity.Question;
using Domain.Entity.Simulacros;

namespace Domain.Port.Simulacro
{
    public interface ISimulacroRepository
    {
        Task<SimulacroEntity?> GetById(string id);
        Task<ResponseEntity<SimulacroEntity>> GetSimulacrosAsync(DateTime fechaActual);
        Task<bool> ExistById(string id);
        Task<List<string>> GenerateQuestionCompetence(int numeroPreguntasByCompetence, string idCompetence);
    }
}