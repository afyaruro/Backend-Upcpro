using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.Simulacro.Commands.GenerarSimulacro
{
    public class GenerarSimulacroCommandValidator : AbstractValidator<GenerarSimulacroInputCommand>
    {
        public GenerarSimulacroCommandValidator()
        {
            
            RuleFor(_ => _.Id)
           .NotNull().WithMessage("El Id del Simulacro no puede ser nulo")
           .NotEmpty().WithMessage("El Id del Simulacro es obligatorio")
           .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id del Simulacro no es válido");

            

        }


    }
}