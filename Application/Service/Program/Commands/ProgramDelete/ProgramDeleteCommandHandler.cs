
using Application.Common.Exceptions;
using Domain.Port.Program;
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramDelete
{
    public class ProgramDeleteCommandHandler
    {
        private readonly IProgramRepository _ProgramRepository;

        public ProgramDeleteCommandHandler(IProgramRepository ProgramRepository)
        {
            this._ProgramRepository = ProgramRepository;
        }

        public async Task<bool> HandleAsync(ProgramDeleteInputCommand command)
        {

            var validator = new ProgramDeleteCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (!await _ProgramRepository.ExistById(command.Id))
            {
                throw new EntityNotFoundException("El programa no existe");
            }



            return await this._ProgramRepository.Delete(command.Id);
        }
    }
}

