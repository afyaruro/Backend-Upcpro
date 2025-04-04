
using Application.Common.Exceptions;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionDelete
{
    public class DeleteInfoQuestionCommandHandler
    {
        private readonly IQuestionRepository<InfoQuestionEntity> _InfoQuestionRepository;

        public DeleteInfoQuestionCommandHandler(IQuestionRepository<InfoQuestionEntity> InfoQuestionRepository)
        {
            this._InfoQuestionRepository = InfoQuestionRepository;
        }

        public async Task<bool> HandleAsync(DeleteInfoQuestionCommand command)
        {

            var validator = new InfoQuestionDelete.DeleteInfoQuestionCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (!await _InfoQuestionRepository.ExistById(command.Id))
            {
                throw new EntityNotFoundException("La informacionde de preguntas no existe");
            }



            return await this._InfoQuestionRepository.Delete(command.Id);
        }
    }
}

