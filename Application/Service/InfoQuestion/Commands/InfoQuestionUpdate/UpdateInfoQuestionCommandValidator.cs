using Application.Base.Validate;
using Application.Service.InfoQuestion.Commands.InfoQuestionUpdate;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionUpdate
{
    public class UpdateInfoQuestionCommandValidator : AbstractValidator<UpdateInfoQuestionCommand>
    {
        public UpdateInfoQuestionCommandValidator()
        {
            RuleFor(_ => _.TypeQuestion).NotNull().WithMessage("El tipo de la pregunta no puede ser nulo")
           .NotEmpty().WithMessage("El tipo de la pregunta es obligatorio")
            .MinimumLength(3).WithMessage("La tipo de la pregunta debe tener minimo 3 caracteres");

            RuleFor(_ => _.Id)
           .NotNull().WithMessage("El Id no puede ser nulo")
           .NotEmpty().WithMessage("El Id es obligatorio")
           .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id no es válido");

        }


    }
}