
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceGetAllPage
{
    public class GetAllPageCompetenceCommandValidator : AbstractValidator<GetAllPageCompetenceInputCommand>
    {
        public GetAllPageCompetenceCommandValidator()
        {
            RuleFor(_ => _.Page)
                .NotNull().WithMessage("El número de página no puede ser nulo")
                .GreaterThan(0).WithMessage("El número de página debe ser mayor que 0");
            RuleFor(_ => _.Size)
                            .NotNull().WithMessage("El número de página no puede ser nulo")
                            .GreaterThan(0).WithMessage("El número de página debe ser mayor que 0");
        }
    }
}