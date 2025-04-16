
using FluentValidation;

namespace Application.Service.Question.Commands.QuestionGetAllPage
{
    public class QuestionGetAllPageSyncCommandValidator : AbstractValidator<QuestionGetAllPageSyncInputCommand>
    {
        public QuestionGetAllPageSyncCommandValidator()
        {
            RuleFor(_ => _.LateDateSync)
                .NotNull().WithMessage("La fecha no puede ser nula.");
        }
    }
}