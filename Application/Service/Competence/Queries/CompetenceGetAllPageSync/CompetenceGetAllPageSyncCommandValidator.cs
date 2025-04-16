
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceGetAllPage
{
    public class CompetenceGetAllPageSyncCommandValidator : AbstractValidator<CompetenceGetAllPageSyncInputCommand>
    {
        public CompetenceGetAllPageSyncCommandValidator()
        {
            RuleFor(_ => _.LateDateSync)
                .NotNull().WithMessage("La fecha no puede ser nula.");
        }
    }
}