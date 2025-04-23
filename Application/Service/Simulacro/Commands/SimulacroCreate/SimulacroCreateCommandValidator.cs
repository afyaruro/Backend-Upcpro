using Application.Base.Validate;
using Application.Service.Simulacro.Commands.SimulacroCreate;
using FluentValidation;

namespace Application.Service.Simulacro.Commands.SimulacroCreate
{
    public class SimulacroCreateCommandValidator : AbstractValidator<SimulacroCreateInputCommand>
    {
        public SimulacroCreateCommandValidator()
        {
            RuleFor(_ => _.Type)
                .NotNull().WithMessage("El tipo de simulacro no puede ser nulo.")
                .NotEmpty().WithMessage("El tipo de simulacro es obligatorio.")
                .MinimumLength(3).WithMessage("El tipo de simulacro debe tener mínimo 3 caracteres.");

            RuleFor(_ => _.Duracion)
                  .NotNull().WithMessage("La duración no puede ser nula")
                  .GreaterThan(0).WithMessage("La duración debe ser mayor a cero");

            RuleFor(x => x.NumeroPreguntas)
                .NotNull().WithMessage("El número de preguntas no puede ser nulo")
                .GreaterThan(0).WithMessage("El número de preguntas debe ser mayor a cero")
                .LessThanOrEqualTo(200).WithMessage("El número de preguntas no puede ser mayor a 200")
                .Must(num => num % 4 == 0)
                .When(x => x.Type == "ALL")
                .WithMessage("El número de preguntas debe ser divisible entre 4");

            RuleFor(_ => _.FechaLimite)
                .NotNull().WithMessage("La fecha límite no puede ser nula")
                .GreaterThan(DateTime.Now).WithMessage("La fecha límite debe ser una fecha futura");
        }
    }
}