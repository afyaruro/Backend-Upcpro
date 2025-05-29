
using Application.Base.PasswordEncryption;
using Application.Base.Validate;
using Application.Common.Exceptions;
using Domain.Entity;
using Domain.Port.User;
using FluentValidation;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserPasswordUpdateCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public UserPasswordUpdateCommandHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<bool> HandleAsync(UserPasswordUpdateInputCommand command, string userId)
        {

            var validator = new UserPasswordUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (command.Password == command.PasswordActual)
            {
                throw new EntityNotFoundException("La contraseña no es valida ya que tu contraseña nueva no puede ser igual a la actual");
            }

            if (!IsValidObjectId.IsValid(userId))
            {
                throw new EntityNotFoundException("El user Id no es valido");
            }

            var existUser = await _userRepository.GetById(userId);

            if (existUser == null)
            {
                throw new EntityNotFoundException("El usuario no esta registrado");
            }

            var helper = new PasswordEncryptionHelper();
            command.PasswordActual = helper.HashPassword(command.PasswordActual, existUser.Mail);

            if (existUser.Password == command.PasswordActual)
            {

                command.Password = helper.HashPassword(command.Password, existUser.Mail);
                return await this._userRepository.UpdatePassword(userId, command.Password);

            }

            return false;

        }


    }
}

