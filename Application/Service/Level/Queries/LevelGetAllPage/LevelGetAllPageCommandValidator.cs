
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class LevelGetAllPageCommandValidator : AbstractValidator<LevelGetAllPageInputCommand>
    {
        public LevelGetAllPageCommandValidator()
        {
            RuleFor(_ => _.Page)
                .NotNull().WithMessage("El número de página no puede ser nulo.")
                .GreaterThanOrEqualTo(0).WithMessage("El número de página debe ser mayor o igual a 0.");

            RuleFor(_ => _.Size)
                .NotNull().WithMessage("El número de registros por página no puede ser nulo.")
                .GreaterThanOrEqualTo(0).WithMessage("El número de registros por página debe ser mayor o igual a 0.");

        }
    }
}