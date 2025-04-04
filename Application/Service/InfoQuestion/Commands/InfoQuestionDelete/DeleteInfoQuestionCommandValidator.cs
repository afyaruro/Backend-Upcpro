using Application.Base.Validate;
using Application.Service.InfoQuestion.Commands.InfoQuestionDelete;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionDelete
{
    public class DeleteInfoQuestionCommandValidator : AbstractValidator<DeleteInfoQuestionCommand>
    {
        public DeleteInfoQuestionCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull().WithMessage("El Id no puede ser nulo")
                .NotEmpty().WithMessage("El Id es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id no es válido");

        }


    }
}
