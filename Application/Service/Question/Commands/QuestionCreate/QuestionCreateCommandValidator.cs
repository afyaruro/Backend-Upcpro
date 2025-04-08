using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.Question.Commands.QuestionCreate
{
    public class QuestionCreateCommandValidator : AbstractValidator<QuestionCreateInputCommand>
    {
        public QuestionCreateCommandValidator()
        {
            RuleFor(_ => _.Enunciated)
                .NotNull().WithMessage("El enunciado no puede ser nulo.")
                .NotEmpty().WithMessage("El enunciado es obligatorio.")
                .MinimumLength(3).WithMessage("El enunciado debe tener mínimo 3 caracteres.");

            RuleFor(_ => _.Feedback)
                .NotNull().WithMessage("La retroalimentación no puede ser nula.")
                .NotEmpty().WithMessage("La retroalimentación es obligatoria.");

            RuleFor(_ => _.OptionType)
                .NotNull().WithMessage("El tipo de opción no puede ser nulo.")
                .NotEmpty().WithMessage("El tipo de opción es obligatorio.");

            RuleFor(_ => _.OptionA)
                .NotNull().WithMessage("La opción A no puede ser nula.")
                .NotEmpty().WithMessage("La opción A es obligatoria.");

            RuleFor(_ => _.OptionB)
                .NotNull().WithMessage("La opción B no puede ser nula.")
                .NotEmpty().WithMessage("La opción B es obligatoria.");

            RuleFor(_ => _.OptionC)
                .NotNull().WithMessage("La opción C no puede ser nula.")
                .NotEmpty().WithMessage("La opción C es obligatoria.");

            RuleFor(_ => _.OptionD)
                .NotNull().WithMessage("La opción D no puede ser nula.")
                .NotEmpty().WithMessage("La opción D es obligatoria.");

            RuleFor(_ => _.CorrectAnswer)
                .InclusiveBetween(1, 4).WithMessage("La respuesta correcta debe estar entre 1 y 4 (correspondiente a A, B, C, D).");

            RuleFor(_ => _.IdInfoQuestion)
                .NotNull().WithMessage("El ID del contexto no puede ser nulo.")
                .NotEmpty().WithMessage("El ID del contexto es obligatorio.")
                 .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id del contexto no es válido");


            RuleFor(_ => _.TypeQuestion)
                .NotNull().WithMessage("El tipo de pregunta no puede ser nulo.")
                .NotEmpty().WithMessage("El tipo de pregunta es obligatorio.");
        }
    }
}
