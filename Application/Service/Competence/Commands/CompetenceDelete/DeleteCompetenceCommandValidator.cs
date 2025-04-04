using Application.Base.Validate;
using Application.Service.Competence.Commands.CompetenceDelete;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceDelete
{
    public class DeleteCompetenceCommandValidator : AbstractValidator<DeleteCompetenceCommand>
    {
        public DeleteCompetenceCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull().WithMessage("El Id no puede ser nulo")
                .NotEmpty().WithMessage("El Id es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id no es válido");

        }


    }
}
