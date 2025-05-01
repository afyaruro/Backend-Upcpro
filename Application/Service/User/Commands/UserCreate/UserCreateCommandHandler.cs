
using Application.Base.PasswordEncryption;
using Application.Common.Exceptions;
using Domain.Entity;
using Domain.Port.User;
using FluentValidation;

namespace Application.Service.User.Commands.UserCreate
{
    public class UserCreateCommandHandler
    {
        private readonly IUserRepository _UserRepository;

        public UserCreateCommandHandler(IUserRepository UserRepository)
        {
            this._UserRepository = UserRepository;
        }

        public async Task<UserCreateOutputCommand> HandleAsync(UserCreateInputCommand command, string typeUser)
        {

            var validator = new UserCreateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _UserRepository.ExistByMail(command.Mail.ToUpper()))
            {
                throw new EntityExistException("Ya existe un usuario registrado con el email.");
            }

            var user = this.toEntity(command, typeUser);

            var resp = await this._UserRepository.Add(user);

            return toCreateOutput(resp);
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

        private UserEntity toEntity(UserCreateInputCommand command, string typeUser)
        {
            var user = new UserEntity();
            user.Identification = command.Identification;
            user.TypeIdentification = command.TypeIdentification;
            var helper = new PasswordEncryptionHelper();
            user.Password = helper.HashPassword(command.Password, command.Mail);
            user.Mail = command.Mail.ToUpper();
            user.Image = command.Image;
            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.IdProgram = command.IdProgram;
            user.TypeUser = typeUser;
            user.Gender = command.Gender;


            return user;

        }
    }
}

