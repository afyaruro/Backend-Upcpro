
using Application.Base.PasswordEncryption;
using Application.Base.Validate;
using Application.Common.Exceptions;
using Domain.Entity;
using Domain.Port.User;
using FluentValidation;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserPasswordByMailUpdateCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public UserPasswordByMailUpdateCommandHandler(IUserRepository UserRepository)
        {
            this._userRepository = UserRepository;
        }

        public async Task<bool> HandleAsync(UserPasswordByMailUpdateInputCommand command)
        {

            var validator = new UserPasswordByMailUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existUser = await _userRepository.GetByMail(command.Mail);

            if (existUser == null)
            {
                throw new EntityNotFoundException("El usuario no esta registrado");
            }

            var helper = new PasswordEncryptionHelper();
            command.Password = helper.HashPassword(command.Password, existUser.Mail);

            return await this._userRepository.UpdatePassword(existUser.Id, command.Password);
        }


    }
}

