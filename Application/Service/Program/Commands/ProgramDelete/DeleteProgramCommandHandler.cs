
using Application.Common.Exceptions;
using Domain.Port.Program;
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramDelete
{
    public class DeleteProgramCommandHandler
    {
        private readonly IProgramRepository _ProgramRepository;

        public DeleteProgramCommandHandler(IProgramRepository ProgramRepository)
        {
            this._ProgramRepository = ProgramRepository;
        }

        public async Task<bool> HandleAsync(DeleteProgramCommand command)
        {

            var validator = new ProgramDelete.DeleteProgramCommandValidator();
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

