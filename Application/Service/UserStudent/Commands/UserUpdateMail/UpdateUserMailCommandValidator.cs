using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UpdateUserMailCommandValidator : AbstractValidator<UpdateUserMailCommand>
    {
        public UpdateUserMailCommandValidator()
        {

            RuleFor(_ => _.Mail)
                            .NotNull().WithMessage("El mail no puede ser nulo")
                            .NotEmpty().WithMessage("El mail es obligatorio")
                            .Length(3, 50).WithMessage("El mail debe tener entre 3 y 50 caracteres")
                            .EmailAddress().WithMessage("El mail no es válido")
                            .Must(mail => mail.EndsWith("@unicesar.edu.co"))
                            .WithMessage("El mail debe ser de dominio @unicesar.edu.co");

        }


    }
}