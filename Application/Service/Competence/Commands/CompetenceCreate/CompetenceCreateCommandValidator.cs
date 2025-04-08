using Application.Service.Competence.Commands.CompetenceCreate;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceCreate
{
    public class CompetenceCreateCommandValidator : AbstractValidator<CompetenceCreateInputCommand>
    {
        public CompetenceCreateCommandValidator()
        {
            RuleFor(_ => _.Name).NotNull().WithMessage($"El nombre de la competencia no puede ser nulo")
                .NotEmpty().WithMessage($"El nombre de la competencia es obligatorio")
                .MinimumLength(3).WithMessage($"El nombre de la competencia debe tener minimo 3 caracteres");
        }
    }
}