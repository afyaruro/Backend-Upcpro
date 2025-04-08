using Application.Base.Validate;
using Application.Service.Competence.Commands.CompetenceUpdate;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceUpdate
{
    public class CompetenceUpdateCommandValidator : AbstractValidator<CompetenceUpdateInputCommand>
    {
        public CompetenceUpdateCommandValidator()
        {
            RuleFor(_ => _.Name).NotNull().WithMessage("El nombre de la competencia no puede ser nulo")
                .NotEmpty().WithMessage("El nombre de la competencia es obligatorio")
                .MinimumLength(3).WithMessage("El nombre de la competencia debe tener minimo 3 caracteres");

            RuleFor(_ => _.Id)
           .NotNull().WithMessage("El Id de la competencia no puede ser nulo")
           .NotEmpty().WithMessage("El Id de la competencia es obligatorio")
           .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id de la competencia no es válido");

        }


    }
}