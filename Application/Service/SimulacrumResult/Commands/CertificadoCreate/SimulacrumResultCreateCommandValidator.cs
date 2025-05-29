
using Application.Base.Validate;
using Application.Service.SimulacrumResult.Commands.SimulacrumResultCreate;
using FluentValidation;

namespace Application.Service.SimulacrumResult.Commands.SimulacrumResultCreate
{
    public class SimulacrumResultCreateCommandValidator : AbstractValidator<SimulacrumResultCreateInputCommand>
    {
        public SimulacrumResultCreateCommandValidator()
        {
            RuleFor(_ => _.IdSimulacro)
                .NotNull().WithMessage("El Id del simulacro no puede ser nulo")
                .NotEmpty().WithMessage("El Id del simulacro es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id del simulacro no es válido");

            RuleFor(_ => _.Duracion)
                     .NotNull().WithMessage("La duración no puede ser nula")
                     .NotEmpty().WithMessage("La duración es obligatoria")
                     .GreaterThanOrEqualTo(0).WithMessage("La duración debe ser mayor o igual a cero");

            RuleFor(_ => _.Fecha)
                .NotNull().WithMessage("La fecha no puede ser nula")
                .NotEmpty().WithMessage("La fecha es obligatoria");
        }
    }
}