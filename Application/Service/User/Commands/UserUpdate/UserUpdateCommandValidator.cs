using Application.Base.Validate;
using Application.Service.User.Commands.UserUpdate;
using FluentValidation;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserUpdateCommandValidator : AbstractValidator<UserUpdateInputCommand>
    {
        public UserUpdateCommandValidator()
        {

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
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El programa no es válido");

        }


    }
}