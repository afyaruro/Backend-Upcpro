
using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.Certificado.Commands.CertificadoCreate
{
    public class CertificadoCreateCommandValidator : AbstractValidator<CertificadoCreateInputCommand>
    {
        public CertificadoCreateCommandValidator()
        {
            RuleFor(_ => _.IdSimulacro)
                .NotNull().WithMessage("El Id del simulacro no puede ser nulo")
                .NotEmpty().WithMessage("El Id del simulacro es obligatorio")
                .Must(id => IsValidObjectId.IsValid(id)).WithMessage("El Id del simulacro no es válido");

            RuleFor(_ => _.Duracion)
                     .NotNull().WithMessage("La duración no puede ser nula")
                     .NotEmpty().WithMessage("La duración es obligatoria")
                     .GreaterThanOrEqualTo(0).WithMessage("La duración debe ser mayor o igual a cero");

            RuleFor(_ => _.Fecha)
                .NotNull().WithMessage("La fecha no puede ser nula")
                .NotEmpty().WithMessage("La fecha es obligatoria");

            RuleFor(_ => _.TotalLectura)
                   .NotNull().WithMessage("El total de preguntas de Lectura Critica no puede ser nulo")
                   .NotEmpty().WithMessage("El total de preguntas de Lectura Critica es obligatorio");


            RuleFor(_ => _.TotalCiudadanas)
                           .NotNull().WithMessage("El total de preguntas de Competencias Ciudadanas no puede ser nulo")
                           .NotEmpty().WithMessage("El total de preguntas de Competencias Ciudadanas es obligatorio")
                           .GreaterThanOrEqualTo(0).WithMessage("El total de preguntas de Competencias Ciudadanas debe ser mayor o igual a cero");

            RuleFor(_ => _.TotalIngles)
                           .NotNull().WithMessage("El total de preguntas de Ingles no puede ser nulo")
                           .NotEmpty().WithMessage("El total de preguntas de Ingles es obligatorio")
                           .GreaterThanOrEqualTo(0).WithMessage("El total de preguntas de Ingles debe ser mayor o igual a cero");

            RuleFor(_ => _.TotalRazonamiento)
                           .NotNull().WithMessage("El total de preguntas de Razonamiento Cuantitativo no puede ser nulo")
                           .NotEmpty().WithMessage("El total de preguntas de Razonamiento Cuantitativo es obligatorio")
                           .GreaterThanOrEqualTo(0).WithMessage("El total de preguntas de Razonamiento Cuantitativo debe ser mayor o igual a cero");

            RuleFor(_ => _.Puntaje)
                           .NotNull().WithMessage("El puntaje no puede ser nulo")
                           .NotEmpty().WithMessage("El puntaje es obligatorio")
                           .GreaterThanOrEqualTo(0).WithMessage("El puntaje debe ser mayor o igual a cero");

            RuleFor(_ => _.JsonQuestions)
                            .NotNull().WithMessage("Las preguntas en formato json no puedes ser nulo")
                            .NotEmpty().WithMessage("Las preguntas en formato json son obligatorias");

        }
    }
}