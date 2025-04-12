
using Application.Base.Validate;
using Application.Service.ResultLevel.Commands.GetRanking;
using FluentValidation;

namespace Application.Service.ResultLevel.Commands.GetRanking
{
    public class RankingCommandValidator : AbstractValidator<RankingInputCommand>
    {
        public RankingCommandValidator()
        {
           
            RuleFor(_ => _.IdCompetence)
                .NotNull().WithMessage($"El Id de la competencia no puede ser nula")
                .NotEmpty().WithMessage($"El Id de la competencia es obligatoria")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage($"El Id de la competencia no es válida");

        }
    }
}