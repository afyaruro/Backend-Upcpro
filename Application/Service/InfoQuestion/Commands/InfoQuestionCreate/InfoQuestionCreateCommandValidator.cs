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
        }
    }
}