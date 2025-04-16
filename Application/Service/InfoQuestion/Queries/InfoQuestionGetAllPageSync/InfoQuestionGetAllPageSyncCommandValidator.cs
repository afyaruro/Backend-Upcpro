
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionGetAllPage
{
    public class InfoQuestionGetAllPageSyncCommandValidator : AbstractValidator<InfoQuestionGetAllPageSyncInputCommand>
    {
        public InfoQuestionGetAllPageSyncCommandValidator()
        {
            RuleFor(_ => _.LateDateSync)
                .NotNull().WithMessage("La fecha no puede ser nula.");
        }
    }
}