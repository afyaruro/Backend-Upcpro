using Application.Base.Validate;
using Application.Service.Faculty.Commands.FacultyDelete;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyDelete
{
    public class DeleteFacultyCommandValidator : AbstractValidator<DeleteFacultyCommand>
    {
        public DeleteFacultyCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull().WithMessage("El Id no puede ser nulo")
                .NotEmpty().WithMessage("El Id es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id no es válido");

        }


    }
}
