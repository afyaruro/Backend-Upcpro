using Application.Service.Simulacro.Commands.GenerarSimulacro;
using Application.Service.Simulacro.Commands.SimulacroCreate;
using Application.Service.Simulacro.Commands.SimulacroDelete;
using Application.Service.Simulacro.Commands.SimulacroGet;
using Application.Service.Simulacro.Commands.SimulacroUpdate;
using Domain.Base.ResponseEntity;
using Domain.Entity.Simulacros;
using Domain.Port.Competence;
using Domain.Port.Simulacro;

namespace Application.Service.Simulacro
{
    public class SimulacroService
    {
        private readonly ISimulacroRepository _repository;
        private readonly ICompetenceRepository _competenceRepository;

        public SimulacroService(ISimulacroRepository repository, ICompetenceRepository competenceRepository)
        {
            this._repository = repository;
            this._competenceRepository = competenceRepository;
        }

        public async Task<SimulacroCreateOutputCommand> Create(SimulacroCreateInputCommand command)
        {
            var _create = new SimulacroCreateCommandHandler(_repository);
            return await _create.HandleAsync(command);
        }

        public async Task<ResponseEntity<SimulacroEntity>> GetAllActive(SimulacroGetInputCommand command)
        {
            var _getAll = new SimulacroGetCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> Update(SimulacroUpdateInputCommand command)
        {
            var _update = new SimulacroUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<List<string>> GenerarSimulacro(GenerarSimulacroInputCommand command)
        {
            var _generar = new GenerarSimulacroCommandHandler(_repository, _competenceRepository);
            return await _generar.HandleAsync(command);
        }

        public async Task<bool> Delete(SimulacroDeleteInputCommand command)
        {
            var _delete = new SimulacroDeleteCommandHandler(_repository);
            return await _delete.HandleAsync(command);
        }


    }
}