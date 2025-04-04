using Application.Base.Validate;
using Application.Service.User.Commands.UserCreate;
using FluentValidation;

namespace Application.Service.User.Commands.UserCreate
{
    public class CreateUserCommandValidator : AbstractValidator<CreateInputUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(_ => _.Mail)
                .NotNull().WithMessage("El mail no puede ser nulo")
                .NotEmpty().WithMessage("El mail es obligatorio")
                .Length(3, 50).WithMessage("El mail debe tener entre 3 y 50 caracteres")
                .EmailAddress().WithMessage("El mail no es válido")
                .Must(mail => mail.EndsWith("@unicesar.edu.co"))
                .WithMessage("El mail debe ser de dominio @unicesar.edu.co");

            RuleFor(_ => _.Image).NotNull().WithMessage("La url de la imagen no puede ser nula")
                .NotEmpty().WithMessage("La url de la imagen es obligatoria")
                .MinimumLength(3).WithMessage("La url de la imagen debe tener minimo 3 caracteres");

            RuleFor(_ => _.Password)
                .NotNull().WithMessage("La contraseña no puede ser nula")
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
                .Matches(@"(?=.*[a-z])").WithMessage("La contraseña debe contener al menos una letra minúscula")
                .Matches(@"(?=.*[A-Z])").WithMessage("La contraseña debe contener al menos una letra mayúscula")
                .Matches(@"(?=.*\d)").WithMessage("La contraseña debe contener al menos un número");

            RuleFor(_ => _.FirstName).NotNull().WithMessage("El nombre no puede ser nulo")
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MinimumLength(3).WithMessage("El nombre debe tener almenos 3 caracteres");

            RuleFor(_ => _.LastName).NotNull().WithMessage("El apellido no puede ser nulo")
                .NotEmpty().WithMessage("El apellido es obligatorio")
                .MinimumLength(3).WithMessage("El apellido debe tener almenos 3 caracteres");

            RuleFor(_ => _.Identification).NotNull().WithMessage("El numero de identificación no puede ser nulo")
                .NotEmpty().WithMessage("El numero de identificación es obligatorio")
                .MinimumLength(5).WithMessage("El numero de identificación debe tener almenos 5 caracteres");

            RuleFor(_ => _.TypeIdentification).NotNull().WithMessage("El tipo de identificación no puede ser nulo")
                .NotEmpty().WithMessage("El tipo de identificación es obligatorio")
                .MinimumLength(3).WithMessage("El tipo de identificación debe tener almenos 3 caracteres");

            RuleFor(_ => _.Gender).NotNull().WithMessage("El genero no puede ser nulo")
                .NotEmpty().WithMessage("El genero es obligatorio")
                .MinimumLength(3).WithMessage("El genero debe tener almenos 3 caracteres");

            RuleFor(_ => _.IdProgram)
                .NotNull().WithMessage("El programa no puede ser nulo")
                .NotEmpty().WithMessage("El programa obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El programa es no válido");

        }
    }
}