using Application.Base.Validate;
using Application.Service.Program.Commands.ProgramCreate;
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramCreate
{
    public class CreateProgramCommandValidator : AbstractValidator<CreateInputProgramCommand>
    {
        public CreateProgramCommandValidator()
        {
            RuleFor(_ => _.Name).NotNull().WithMessage("El Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El Nombre es obligatorio")
                .Length(3, 50).WithMessage("El Nombre debe tener entre 3 y 50 caracteres");

            RuleFor(_ => _.IdFaculty)
                .NotNull().WithMessage("El Id de la facultad no puede ser nulo")
                .NotEmpty().WithMessage("El Id de la facultad es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id de la facultad no es válido");

        }
    }
}