
using Application.Service.User.Commands.UserLogin;
using FluentValidation;

namespace Application.Service.User.Commands.User
{
    public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator()
        {

            RuleFor(_ => _.Mail)
                    .NotNull().WithMessage("El mail no puede ser nulo")
                    .NotEmpty().WithMessage("El mail es obligatorio");

            RuleFor(_ => _.Password)
                    .NotNull().WithMessage("La contraseña no puede ser nula")
                    .NotEmpty().WithMessage("La contraseña es obligatoria");

        }


    }
}