
using FluentValidation;

namespace Application.Service.Level.Commands.LevelCreate
{
    public class LevelCreateCommandValidator : AbstractValidator<LevelCreateInputCommand>
    {
        public LevelCreateCommandValidator()
        {
            RuleFor(_ => _.Level).GreaterThan(0)
                .WithMessage("El nivel debe ser mayor que 0");

            RuleFor(_ => _.Dificulty).InclusiveBetween(1, 10)
                .WithMessage("La dificultad debe estar entre 1 y 10");

            RuleFor(_ => _.Reward).GreaterThanOrEqualTo(0)
                .WithMessage("La recompensa no puede ser negativa");

            RuleFor(_ => _.NumQuestion).GreaterThan(0)
            .WithMessage("El numero de preguntas debe ser mayor que 0");

        }
    }
}