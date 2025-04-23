
using Application.Common.Exceptions;
using Domain.Entity.Simulacros;
using Domain.Port.Simulacro;
using FluentValidation;

namespace Application.Service.Simulacro.Commands.SimulacroUpdate
{
    public class SimulacroUpdateCommandHandler
    {
        private readonly ISimulacroRepository _simulacroRepository;

        public SimulacroUpdateCommandHandler(ISimulacroRepository simulacroRepository)
        {
            this._simulacroRepository = simulacroRepository;
        }

        public async Task<bool> HandleAsync(SimulacroUpdateInputCommand command)
        {

            var validator = new SimulacroUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var Simulacro = new SimulacroEntity(
                command.Duracion,
                command.NumeroPreguntas,
                command.FechaLimite,
                command.Type
            );

            Simulacro.Id = command.Id;
            Simulacro.DateUpdate = DateTime.Now;


            if (!await _simulacroRepository.ExistById(Simulacro.Id))
            {
                throw new EntityNotFoundException("El Simulacro a actualizar no existe");
            }

            return await this._simulacroRepository.ActualizarAsync(Simulacro);
        }
    }
}

