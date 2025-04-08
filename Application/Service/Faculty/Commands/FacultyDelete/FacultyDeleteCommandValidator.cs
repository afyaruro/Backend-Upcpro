using Application.Base.Validate;
using Application.Service.Faculty.Commands.FacultyDelete;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyDelete
{
    public class FacultyDeleteCommandValidator : AbstractValidator<FacultyDeleteInputCommand>
    {
        public FacultyDeleteCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull().WithMessage("El Id de la facultad no puede ser nulo")
                .NotEmpty().WithMessage("El Id de la facultad es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id de la facultad no es válido");

        }


    }
}
