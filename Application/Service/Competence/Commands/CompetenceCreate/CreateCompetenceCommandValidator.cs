using Application.Service.Competence.Commands.CompetenceCreate;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceCreate
{
    public class CreateCompetenceCommandValidator : AbstractValidator<CreateInputCompetenceCommand>
    {
        public CreateCompetenceCommandValidator()
        {
            RuleFor(_ => _.Name).NotNull().WithMessage("El Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El Nombre es obligatorio")
                .Length(3, 50).WithMessage("El Nombre debe tener entre 3 y 50 caracteres");
        }
    }
}