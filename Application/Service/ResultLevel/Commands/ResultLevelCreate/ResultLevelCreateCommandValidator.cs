
using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.ResultLevel.Commands.ResultLevelCreate
{
    public class ResultLevelCreateCommandValidator : AbstractValidator<ResultLevelCreateInputCommand>
    {
        public ResultLevelCreateCommandValidator()
        {

          
            RuleFor(_ => _.UserId)
                       .NotNull().WithMessage($"El Id del usuario no puede ser nulo")
                       .NotEmpty().WithMessage($"El Id del usuario es obligatorio")
                       .Must(id => IsValidObjectId.IsValid(id)).WithMessage($"El Id del usuario no es válido");

        }
    }
}