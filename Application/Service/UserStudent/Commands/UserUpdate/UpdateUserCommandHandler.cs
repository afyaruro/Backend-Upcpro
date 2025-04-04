
using Application.Base.Validate;
using Application.Common.Exceptions;
using Domain.Entity;
using Domain.Port.User;
using FluentValidation;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UpdateUserCommandHandler
    {
        private readonly IUserRepository _UserRepository;

        public UpdateUserCommandHandler(IUserRepository UserRepository)
        {
            this._UserRepository = UserRepository;
        }

        public async Task<bool> HandleAsync(UpdateUserCommand command, string userId)
        {

            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (!IsValidObjectId.IsValid(userId))
            {
                throw new EntityNotFoundException("El user Id no es valido");
            }

            var user = this.toEntity(command);
            user.Id = userId;



            if (!await _UserRepository.ExistById(user.Id))
            {
                throw new EntityNotFoundException("El usuario a actualizar no existe");
            }


            return await this._UserRepository.Update(user);
        }

        private UserEntity toEntity(UpdateUserCommand command)
        {
            var user = new UserEntity();
            user.Identification = command.Identification;
            user.TypeIdentification = command.TypeIdentification;
            user.Image = command.Image;
            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.IdProgram = command.IdProgram;
            user.Gender = command.Gender;
            user.DateUpdate = DateTime.Now;

            return user;

        }
    }
}

