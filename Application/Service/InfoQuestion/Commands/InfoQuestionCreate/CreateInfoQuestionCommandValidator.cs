using Application.Service.InfoQuestion.Commands.InfoQuestionCreate;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionCreate
{
    public class CreateInfoQuestionCommandValidator : AbstractValidator<CreateInputInfoQuestionCommand>
    {
        public CreateInfoQuestionCommandValidator()
        {

            RuleFor(_ => _.TypeQuestion).NotNull().WithMessage("El tipo de la pregunta no puede ser nulo")
           .NotEmpty().WithMessage("El tipo de la pregunta es obligatorio")
            .MinimumLength(3).WithMessage("La tipo de la pregunta debe tener minimo 3 caracteres");
        }
    }
}