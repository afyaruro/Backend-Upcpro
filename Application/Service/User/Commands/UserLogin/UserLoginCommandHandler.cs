
using Application.Base.PasswordEncryption;
using Application.Base.Validate;
using Application.Common.Exceptions;
using Application.Service.User.Commands.UserCreate;
using Application.Service.User.Commands.UserLogin;
using Domain.Base.ResponseEntity;
using Domain.Entity;
using Domain.Port.User;
using FluentValidation;

namespace Application.Service.User.Commands.User
{
    public class UserLoginCommandHandler
    {
        private readonly IUserRepository _UserRepository;

        public UserLoginCommandHandler(IUserRepository UserRepository)
        {
            this._UserRepository = UserRepository;
        }

        public async Task<ResponseEntity<UserCreateOutputCommand>> HandleAsync(UserLoginCommand command)
        {

            var validator = new UserLoginCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            command.Mail = command.Mail.ToUpper();

            var existUser = await _UserRepository.GetByMail(command.Mail);

            if (existUser == null)
            {
                throw new EntityNotFoundException("El usuario no esta registrado");
            }

            var helper = new PasswordEncryptionHelper();
            bool isValid = helper.VerifyPassword(existUser.Password, command.Password, command.Mail);
            if (isValid)
            {
                var createOutput = this.toCreateOutput(existUser);
                var response = new ResponseEntity<UserCreateOutputCommand>("Credenciales correctas", createOutput);

                return response;
            }
            else
            {
                throw new NotAuthException("Contraseña incorrecta");
            }

        }

        private UserCreateOutputCommand toCreateOutput(UserEntity user)
        {
            var command = new UserCreateOutputCommand();
            command.Identification = user.Identification;
            command.TypeIdentification = user.TypeIdentification;
            command.Mail = user.Mail;
            command.Image = user.Image;
            command.FirstName = user.FirstName;
            command.LastName = user.LastName;
            command.Program = user.Program;
            command.TypeUser = user.TypeUser;
            command.Gender = user.Gender;
            command.Id = user.Id;
            command.Puntaje = user.Puntaje;


            return command;

        }


    }
}

