using Application.Base.Validate;
using FluentValidation;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserPuntajeUpdateCommandValidator : AbstractValidator<UserPuntajeUpdateInputCommand>
    {
        public UserPuntajeUpdateCommandValidator()
        {

            RuleFor(_ => _.Puntaje)
               .NotNull().WithMessage("El puntaje no puede ser nulo")
               .NotEmpty().WithMessage("El puntaje es obligatorio");


        }


    }
}