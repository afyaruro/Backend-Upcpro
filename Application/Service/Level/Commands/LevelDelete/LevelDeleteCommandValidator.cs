using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.Level.Commands.LevelDelete
{
    public class LevelDeleteCommandValidator : AbstractValidator<LevelDeleteInputCommand>
    {
        public LevelDeleteCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull().WithMessage($"El Id del nivel no puede ser nulo")
                .NotEmpty().WithMessage($"El Id del nivel es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage($"El Id del nivel no es válido");

        }


    }
}
