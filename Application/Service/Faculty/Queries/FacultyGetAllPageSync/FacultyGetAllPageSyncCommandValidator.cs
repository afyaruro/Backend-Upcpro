
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class FacultyGetAllPageSyncCommandValidator : AbstractValidator<FacultyGetAllPageSyncInputCommand>
    {
        public FacultyGetAllPageSyncCommandValidator()
        {
            RuleFor(_ => _.LateDateSync)
                .NotNull().WithMessage("La fecha no puede ser nula.");
        }
    }
}