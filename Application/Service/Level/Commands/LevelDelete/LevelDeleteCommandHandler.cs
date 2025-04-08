
using Application.Common.Exceptions;
using Domain.Port.Level;
using FluentValidation;

namespace Application.Service.Level.Commands.LevelDelete
{
    public class LevelDeleteCommandHandler
    {
        private readonly ILevelRepository _LevelRepository;

        public LevelDeleteCommandHandler(ILevelRepository LevelRepository)
        {
            this._LevelRepository = LevelRepository;
        }

        public async Task<bool> HandleAsync(LevelDeleteInputCommand command)
        {

            var validator = new LevelDeleteCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (!await _LevelRepository.ExistById(command.Id))
            {
                throw new EntityNotFoundException("El nivel a eliminar no existe");
            }

            return await this._LevelRepository.Delete(command.Id);
        }
    }
}

