using Application.Base.Validate;
using Application.Service.Program.Commands.ProgramDelete;
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramDelete
{
    public class DeleteProgramCommandValidator : AbstractValidator<DeleteProgramCommand>
    {
        public DeleteProgramCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull().WithMessage("El Id no puede ser nulo")
                .NotEmpty().WithMessage("El Id es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id no es válido");

        }


    }
}
