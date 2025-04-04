using Application.Service.Faculty.Commands.FacultyCreate;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyCreate
{
    public class CreateFacultyCommandValidator : AbstractValidator<CreateInputFacultyCommand>
    {
        public CreateFacultyCommandValidator()
        {
            RuleFor(_ => _.Name).NotNull().WithMessage("El Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El Nombre es obligatorio")
                .Length(3, 50).WithMessage("El Nombre debe tener entre 3 y 50 caracteres");
        }
    }
}