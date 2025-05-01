using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserPasswordByMailUpdateCommandValidator : AbstractValidator<UserPasswordByMailUpdateInputCommand>
    {
        public UserPasswordByMailUpdateCommandValidator()
        {

            RuleFor(_ => _.Password)
               .NotNull().WithMessage("La contraseña no puede ser nula")
               .NotEmpty().WithMessage("La contraseña es obligatoria")
               .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
               .Matches(@"(?=.*[a-z])").WithMessage("La contraseña debe contener al menos una letra minúscula")
               .Matches(@"(?=.*[A-Z])").WithMessage("La contraseña debe contener al menos una letra mayúscula")
               .Matches(@"(?=.*\d)").WithMessage("La contraseña debe contener al menos un número");

            RuleFor(_ => _.Mail)
                          .NotNull().WithMessage("El correo es obligatorio");


        }


    }
}