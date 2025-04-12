using Domain.Base.ResponseEntity;
using Domain.Entity.Simulacros;
using Domain.Port.Simulacro;
using FluentValidation;

namespace Application.Service.Simulacro.Commands.SimulacroGet
{
    public class SimulacroGetCommandHandler
    {
        private readonly ISimulacroRepository _SimulacroRepository;

        public SimulacroGetCommandHandler(ISimulacroRepository SimulacroRepository)
        {
            this._SimulacroRepository = SimulacroRepository;
        }

        public async Task<ResponseEntity<SimulacroEntity>> HandleAsync(SimulacroGetInputCommand command)
        {

            var validator = new SimulacroGetCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await this._SimulacroRepository.GetSimulacrosAsync(command.FechaActual);
        }

        
    }
}

