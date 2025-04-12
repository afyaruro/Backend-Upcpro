
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

            RuleFor(_ => _.NumCorrectasCiudadanas)
                .NotNull().WithMessage("El número de respuestas correctas en Ciudadanas no puede ser nulo")
                .NotEmpty().WithMessage("El número de respuestas correctas en Ciudadanas es obligatorio")
                .GreaterThanOrEqualTo(0).WithMessage("El número de respuestas correctas en Ciudadanas debe ser mayor o igual a cero");

            RuleFor(_ => _.NumCorrectasIngles)
                .NotNull().WithMessage("El número de respuestas correctas en Inglés no puede ser nulo")
                .NotEmpty().WithMessage("El número de respuestas correctas en Inglés es obligatorio")
                .GreaterThanOrEqualTo(0).WithMessage("El número de respuestas correctas en Inglés debe ser mayor o igual a cero");

            RuleFor(_ => _.NumCorrectasRazonamiento)
                .NotNull().WithMessage("El número de respuestas correctas en Razonamiento no puede ser nulo")
                .NotEmpty().WithMessage("El número de respuestas correctas en Razonamiento es obligatorio")
                .GreaterThanOrEqualTo(0).WithMessage("El número de respuestas correctas en Razonamiento debe ser mayor o igual a cero");

            RuleFor(_ => _.NumCorrectasLectura)
                .NotNull().WithMessage("El número de respuestas correctas en Lectura no puede ser nulo")
                .NotEmpty().WithMessage("El número de respuestas correctas en Lectura es obligatorio")
                .GreaterThanOrEqualTo(0).WithMessage("El número de respuestas correctas en Lectura debe ser mayor o igual a cero");
        }
    }
}