using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserMailUpdateCommandValidator : AbstractValidator<UserMailUpdateInputCommand>
    {
        public UserMailUpdateCommandValidator()
        {

            RuleFor(_ => _.Mail)
                            .NotNull().WithMessage("El mail no puede ser nulo")
                            .NotEmpty().WithMessage("El mail es obligatorio")
                            .MinimumLength(3).WithMessage("El mail debe tener minimo 3 caracteres")
                            .EmailAddress().WithMessage("El mail no es válido")
                            .Must(mail => mail.EndsWith("@unicesar.edu.co"))
                            .WithMessage("El mail debe ser de dominio @unicesar.edu.co");

        }


    }
}