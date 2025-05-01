
using Application.Base.Validate;
using Application.Common.Exceptions;
using Domain.Entity;
using Domain.Port.User;
using FluentValidation;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserPuntajeUpdateCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public UserPuntajeUpdateCommandHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<bool> HandleAsync(UserPuntajeUpdateInputCommand command, string userId)
        {

            var validator = new UserPuntajeUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existUser = await _userRepository.GetById(userId);

            if (existUser == null)
            {
                throw new EntityNotFoundException("El usuario no esta registrado");
            }

            return await this._userRepository.UpdatePuntaje(userId, command.Puntaje);
        }


    }
}

