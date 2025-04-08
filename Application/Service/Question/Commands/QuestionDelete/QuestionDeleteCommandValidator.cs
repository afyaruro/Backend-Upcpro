using Application.Base.Validate;
using Application.Service.Question.Commands.QuestionDelete;
using FluentValidation;

namespace Application.Service.Question.Commands.QuestionDelete
{
    public class QuestionDeleteCommandValidator : AbstractValidator<QuestionDeleteInputCommand>
    {
        public QuestionDeleteCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull().WithMessage("El Id de la informacion de las preguntas no puede ser nulo")
                .NotEmpty().WithMessage("El Id de la informacion de las preguntas es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id de la informacion de las preguntas no es válido");

        }


    }
}
