
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramGetAllPage
{
    public class ProgramGetAllPageSyncCommandValidator : AbstractValidator<ProgramGetAllPageSyncInputCommand>
    {
        public ProgramGetAllPageSyncCommandValidator()
        {
            RuleFor(_ => _.LateDateSync)
                .NotNull().WithMessage("La fecha no puede ser nula.");
        }
    }
}