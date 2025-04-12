
using Domain.Entity.Simulacros;
using Domain.Port.Simulacro;
using FluentValidation;

namespace Application.Service.Simulacro.Commands.SimulacroCreate
{
    public class SimulacroCreateCommandHandler
    {
        private readonly ISimulacroRepository _simulacroRepository;


        public SimulacroCreateCommandHandler(ISimulacroRepository simulacroRepository)
        {
            this._simulacroRepository = simulacroRepository;
        }

        public async Task<SimulacroCreateOutputCommand> HandleAsync(SimulacroCreateInputCommand command)
        {

            var validator = new SimulacroCreateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var resp = await this._simulacroRepository.CrearAsync(new SimulacroEntity(duracion: command.Duracion, numeroPreguntas: command.NumeroPreguntas, fechaLimite: command.FechaLimite));
            
            return new SimulacroCreateOutputCommand(
                duracion: resp.Duracion,
                numeroPreguntas: resp.NumeroPreguntas,
                fechaLimite: resp.FechaLimite,
                id: resp.Id
            );
        }
    }
}

