
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyCreate
{
    public class FacultyCreateCommandValidator : AbstractValidator<FacultyCreateInputCommand>
    {
        public FacultyCreateCommandValidator()
        {
            RuleFor(_ => _.Name).NotNull().WithMessage("El nombre de la facultad no puede ser nulo")
                .NotEmpty().WithMessage("El nombre de la facultad es obligatorio")
                .MinimumLength(3).WithMessage($"El nombre de la facultad debe tener minimo 3 caracteres");
        }
    }
}