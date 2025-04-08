using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceDelete
{
    public class CompetenceDeleteCommandValidator : AbstractValidator<CompetenceDeleteInputCommand>
    {
        public CompetenceDeleteCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull().WithMessage($"El Id de la competencia no puede ser nulo")
                .NotEmpty().WithMessage($"El Id de la competencia es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage($"El Id de la competencia no es válido");

        }


    }
}
