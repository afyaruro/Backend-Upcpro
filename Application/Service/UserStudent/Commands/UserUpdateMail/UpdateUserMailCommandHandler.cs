
using Application.Base.Validate;
using Application.Common.Exceptions;
using Domain.Entity;
using Domain.Port.User;
using FluentValidation;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UpdateUserMailCommandHandler
    {
        private readonly IUserRepository _UserRepository;

        public UpdateUserMailCommandHandler(IUserRepository UserRepository)
        {
            this._UserRepository = UserRepository;
        }

        public async Task<bool> HandleAsync(UpdateUserMailCommand command, string userId)
        {

            var validator = new UpdateUserMailCommandValidator();
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

            if (await _UserRepository.ExistByMail(command.Mail.ToUpper()) && existUser.Id != userId)
            {
                throw new EntityNotFoundException("ya se encuentra registrado un usuario con el mail");
            }


            return await this._UserRepository.UpdateMail(userId, command.Mail.ToUpper());
        }


    }
}

