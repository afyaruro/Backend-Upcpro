
using Application.Common.Exceptions;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionDelete
{
    public class InfoQuestionDeleteCommandHandler
    {
        private readonly IQuestionRepository<InfoQuestionEntity> _InfoQuestionRepository;

        public InfoQuestionDeleteCommandHandler(IQuestionRepository<InfoQuestionEntity> InfoQuestionRepository)
        {
            this._InfoQuestionRepository = InfoQuestionRepository;
        }

        public async Task<bool> HandleAsync(InfoQuestionDeleteInputCommand command)
        {

            var validator = new InfoQuestionDeleteCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (!await _InfoQuestionRepository.ExistById(command.Id))
            {
                throw new EntityNotFoundException("La informacion de las preguntas no existe");
            }


            return await this._InfoQuestionRepository.Delete(command.Id);
        }
    }
}

//validar que no tenga preguntas asociadas antes de eliminar

