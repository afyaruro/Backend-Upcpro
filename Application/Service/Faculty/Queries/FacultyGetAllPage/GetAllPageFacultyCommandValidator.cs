
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class GetAllPageFacultyCommandValidator : AbstractValidator<GetAllPageFacultyInputCommand>
    {
        public GetAllPageFacultyCommandValidator()
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