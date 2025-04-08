using Application.Base.Validate;
using Application.Service.Program.Commands.ProgramDelete;
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramDelete
{
    public class ProgramDeleteCommandValidator : AbstractValidator<ProgramDeleteInputCommand>
    {
        public ProgramDeleteCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull().WithMessage("El Id del programa no puede ser nulo")
                .NotEmpty().WithMessage("El Id del programa es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id del programa no es válido");

        }


    }
}
