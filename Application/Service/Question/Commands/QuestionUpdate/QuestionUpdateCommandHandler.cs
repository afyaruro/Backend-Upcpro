
using Application.Common.Exceptions;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.Question.Commands.QuestionUpdate
{
    public class QuestionUpdateCommandHandler
    {
        private readonly IQuestionRepository<QuestionEntity> _QuestionRepository;

        public QuestionUpdateCommandHandler(IQuestionRepository<QuestionEntity> QuestionRepository)
        {
            this._QuestionRepository = QuestionRepository;
        }

        public async Task<bool> HandleAsync(QuestionUpdateInputCommand command)
        {

            var validator = new QuestionUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var question = new QuestionEntity(enunciated: command.Enunciated, feedback: command.Feedback, optionType: command.OptionType, optionA: command.OptionA, optionB: command.OptionB, optionC: command.OptionC, optionD: command.OptionD, correctAnswer: command.CorrectAnswer, idInfoQuestion: command.IdInfoQuestion, typeQuestion: command.TypeQuestion);


            question.Id = command.Id;
            question.DateUpdate = DateTime.Now;


            if (!await _QuestionRepository.ExistById(question.Id))
            {
                throw new EntityNotFoundException("La pregunta a actualizar no existe");
            }

            return await this._QuestionRepository.Update(question);
        }
    }
}

