using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionUpdate
{
    public class InfoQuestionUpdateCommandValidator : AbstractValidator<InfoQuestionUpdateInputCommand>
    {
        public InfoQuestionUpdateCommandValidator()
        {
            RuleFor(_ => _.Context).NotNull().WithMessage("El context de la informacion de preguntas no puede ser nulo")
                .NotEmpty().WithMessage("El context de la informacion de preguntas es obligatorio")
                .MinimumLength(3).WithMessage("El context de la informacion de preguntas debe tener minimo 3 caracteres");

            RuleFor(_ => _.Id)
                .NotNull().WithMessage("El Id de la informacion de las preguntas no puede ser nulo")
                .NotEmpty().WithMessage("El Id de la informacion de las preguntas es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id de la informacion de las preguntas no es válido");

         
        }


    }
}