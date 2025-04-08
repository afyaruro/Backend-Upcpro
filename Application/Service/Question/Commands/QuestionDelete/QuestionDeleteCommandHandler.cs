
using Application.Common.Exceptions;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.Question.Commands.QuestionDelete
{
    public class QuestionDeleteCommandHandler
    {
        private readonly IQuestionRepository<QuestionEntity> _QuestionRepository;

        public QuestionDeleteCommandHandler(IQuestionRepository<QuestionEntity> QuestionRepository)
        {
            this._QuestionRepository = QuestionRepository;
        }

        public async Task<bool> HandleAsync(QuestionDeleteInputCommand command)
        {

            var validator = new QuestionDeleteCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (!await _QuestionRepository.ExistById(command.Id))
            {
                throw new EntityNotFoundException("La informacion de las preguntas no existe");
            }


            return await this._QuestionRepository.Delete(command.Id);
        }
    }
}


