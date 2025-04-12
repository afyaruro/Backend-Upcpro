
using Application.Common.Exceptions;
using Domain.Port.Simulacro;
using FluentValidation;

namespace Application.Service.Simulacro.Commands.SimulacroDelete
{
    public class SimulacroDeleteCommandHandler
    {
        private readonly ISimulacroRepository _simulacroRepository;

        public SimulacroDeleteCommandHandler(ISimulacroRepository simulacroRepository)
        {
            this._simulacroRepository = simulacroRepository;
        }

        public async Task<bool> HandleAsync(SimulacroDeleteInputCommand command)
        {

            var validator = new SimulacroDeleteCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (!await _simulacroRepository.ExistById(command.Id))
            {
                throw new EntityNotFoundException("El Simulacro no existe");
            }



            return await this._simulacroRepository.EliminarAsync(command.Id);
        }
    }
}

