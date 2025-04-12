using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.Simulacro.Commands.SimulacroUpdate
{
    public class SimulacroUpdateCommandValidator : AbstractValidator<SimulacroUpdateInputCommand>
    {
        public SimulacroUpdateCommandValidator()
        {
            
            RuleFor(_ => _.Id)
                .NotNull().WithMessage("El Id del Simulacro no puede ser nulo")
                .NotEmpty().WithMessage("El Id del Simulacro es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id del Simulacro no es válido");

             RuleFor(_ => _.Duracion)
                .NotNull().WithMessage("La duración no puede ser nula")
                .GreaterThan(0).WithMessage("La duración debe ser mayor a cero");
                  
             RuleFor(_ => _.NumeroPreguntas)
                .NotNull().WithMessage("El número de preguntas no puede ser nulo")
                .GreaterThan(0).WithMessage("El número de preguntas debe ser mayor a cero")
                .LessThanOrEqualTo(200).WithMessage("El número de preguntas no puede ser mayor a 200")
                .Must(num => num % 4 == 0).WithMessage("El número de preguntas debe ser divisible entre 4");

            RuleFor(_ => _.FechaLimite)
                .NotNull().WithMessage("La fecha límite no puede ser nula")
                .GreaterThan(DateTime.Now).WithMessage("La fecha límite debe ser una fecha futura");

        }


    }
}