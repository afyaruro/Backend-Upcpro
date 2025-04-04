using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramUpdate
{
    public class UpdateProgramCommandValidator : AbstractValidator<UpdateProgramCommand>
    {
        public UpdateProgramCommandValidator()
        {
            RuleFor(_ => _.Name).NotNull().WithMessage("El Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El Nombre es obligatorio")
                .Length(3, 50).WithMessage("El Nombre debe tener entre 3 y 50 caracteres");

            RuleFor(_ => _.Id)
           .NotNull().WithMessage("El Id no puede ser nulo")
           .NotEmpty().WithMessage("El Id es obligatorio")
           .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id no es válido");

            RuleFor(_ => _.IdFaculty)
           .NotNull().WithMessage("El Id de la facultad no puede ser nulo")
           .NotEmpty().WithMessage("El Id de la facultad es obligatorio")
           .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id de la facultad no es válido");

        }


    }
}