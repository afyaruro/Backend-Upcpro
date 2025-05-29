
using FluentValidation;

namespace Application.Service.EspacioFisico.Commands.EspacioFisicoGetAllPage
{
    public class EspacioFisicoGetAllPageSyncCommandValidator : AbstractValidator<EspacioFisicoGetAllPageSyncInputCommand>
    {
        public EspacioFisicoGetAllPageSyncCommandValidator()
        {
            RuleFor(_ => _.LateDateSync)
                .NotNull().WithMessage("La fecha no puede ser nula.");
        }
    }
}