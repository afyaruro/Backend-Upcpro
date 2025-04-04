
using Application.Base.PasswordEncryption;
using Application.Base.Validate;
using Application.Common.Exceptions;
using Domain.Entity;
using Domain.Port.User;
using FluentValidation;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UpdateUserPasswordCommandHandler
    {
        private readonly IUserRepository _UserRepository;

        public UpdateUserPasswordCommandHandler(IUserRepository UserRepository)
        {
            this._UserRepository = UserRepository;
        }

        public async Task<bool> HandleAsync(UpdateUserPasswordCommand command, string userId)
        {

            var validator = new UpdateUserPasswordCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (!IsValidObjectId.IsValid(userId))
            {
                throw new EntityNotFoundException("El user Id no es valido");
            }

            var existUser = await _UserRepository.GetById(userId);

            if (existUser == null)
            {
                throw new EntityNotFoundException("El usuario no esta registrado");
            }

            var helper = new PasswordEncryptionHelper();
            command.Password = helper.HashPassword(command.Password, existUser.Mail);

            return await this._UserRepository.UpdatePassword(userId, command.Password);
        }


    }
}

