using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyUpdate
{
    public class FacultyUpdateCommandValidator : AbstractValidator<FacultyUpdateInputCommand>
    {
        public FacultyUpdateCommandValidator()
        {
            RuleFor(_ => _.Name).NotNull().WithMessage("El nombre de la facultad no puede ser nulo")
                .NotEmpty().WithMessage("El nombre de la facultad es obligatorio")
                .MinimumLength(3).WithMessage($"El nombre de la facultad debe tener minimo 3 caracteres");
        

            RuleFor(_ => _.Id)
           .NotNull().WithMessage("El Id de la facultad no puede ser nulo")
           .NotEmpty().WithMessage("El Id de la facultad es obligatorio")
           .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id de la facultad no es válido");

        }


    }
}