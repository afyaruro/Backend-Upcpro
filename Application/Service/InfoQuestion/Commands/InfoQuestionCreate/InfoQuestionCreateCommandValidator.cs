using Application.Base.Validate;
using Application.Service.InfoQuestion.Commands.InfoQuestionCreate;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionCreate
{
    public class InfoQuestionCreateCommandValidator : AbstractValidator<InfoQuestionCreateInputCommand>
    {
        public InfoQuestionCreateCommandValidator()
        {

            RuleFor(_ => _.Context).NotNull().WithMessage("El context de la informacion de preguntas no puede ser nulo")
           .NotEmpty().WithMessage("El context de la informacion de preguntas es obligatorio")
            .MinimumLength(3).WithMessage("El context de la informacion de preguntas debe tener minimo 3 caracteres");

            RuleFor(_ => _.IdCompetence)
                .NotNull().WithMessage("El Id de la competencia no puede ser nulo")
                .NotEmpty().WithMessage("El Id de la competencia es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id de la  no es válido");

        }
    }
}