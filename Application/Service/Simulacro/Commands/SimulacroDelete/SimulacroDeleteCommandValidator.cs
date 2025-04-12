using Application.Base.Validate;
using Application.Service.Simulacro.Commands.SimulacroDelete;
using FluentValidation;

namespace Application.Service.Simulacro.Commands.SimulacroDelete
{
    public class SimulacroDeleteCommandValidator : AbstractValidator<SimulacroDeleteInputCommand>
    {
        public SimulacroDeleteCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull().WithMessage("El Id del Simulacro no puede ser nulo")
                .NotEmpty().WithMessage("El Id del Simulacro es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id del Simulacro no es válido");

        }


    }
}
