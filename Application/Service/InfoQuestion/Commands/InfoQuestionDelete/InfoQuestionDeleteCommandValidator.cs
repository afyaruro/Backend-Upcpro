using Application.Base.Validate;
using Application.Service.InfoQuestion.Commands.InfoQuestionDelete;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionDelete
{
    public class InfoQuestionDeleteCommandValidator : AbstractValidator<InfoQuestionDeleteInputCommand>
    {
        public InfoQuestionDeleteCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull().WithMessage("El Id de la informacion de las preguntas no puede ser nulo")
                .NotEmpty().WithMessage("El Id de la informacion de las preguntas es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id de la informacion de las preguntas no es válido");

        }


    }
}
