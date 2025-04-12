using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.ResultLevel.Commands.ResultLevelUpdate
{
    public class ResultLevelUpdateCommandValidator : AbstractValidator<ResultLevelUpdateInputCommand>
    {
        public ResultLevelUpdateCommandValidator()
        {
            
            RuleFor(_ => _.Score).GreaterThanOrEqualTo(0)
                .WithMessage("El score no puede ser negativo");

            RuleFor(_ => _.LevelId).NotNull()
                .NotNull().WithMessage($"El Id del nivel no puede ser nulo")
                .NotEmpty().WithMessage($"El Id del nivel es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage($"El Id del nivel no es válido");

            RuleFor(_ => _.IdCompetence)
                .NotNull().WithMessage($"El Id de la competencia no puede ser nulo")
                .NotEmpty().WithMessage($"El Id de la competencia es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage($"El Id de la competencia no es válido");

            
        }


    }
}