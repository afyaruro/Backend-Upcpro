using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.Simulacro.Commands.SimulacroGet
{
    public class SimulacroGetCommandValidator : AbstractValidator<SimulacroGetInputCommand>
    {
        public SimulacroGetCommandValidator()
        {
            
           
            RuleFor(_ => _.FechaActual)
                .NotNull().WithMessage("La fecha actual no puede ser nula");
               

        }


    }
}