using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramUpdate
{
    public class ProgramUpdateCommandValidator : AbstractValidator<ProgramUpdateInputCommand>
    {
        public ProgramUpdateCommandValidator()
        {
            RuleFor(_ => _.Name).NotNull().WithMessage("El nombre del programa no puede ser nulo")
                .NotEmpty().WithMessage("El nombre del programa  es obligatorio")
                 .MinimumLength(3).WithMessage($"El nombre del programa debe tener minimo 3 caracteres");

            RuleFor(_ => _.Id)
           .NotNull().WithMessage("El Id del programa no puede ser nulo")
           .NotEmpty().WithMessage("El Id del programa es obligatorio")
           .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id del programa no es válido");

            RuleFor(_ => _.IdFaculty)
           .NotNull().WithMessage("El Id de la facultad no puede ser nulo")
           .NotEmpty().WithMessage("El Id de la facultad es obligatorio")
           .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id de la facultad no es válido");

        }


    }
}