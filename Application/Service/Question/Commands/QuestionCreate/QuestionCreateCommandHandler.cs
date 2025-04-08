
using Application.Common.Exceptions;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.Question.Commands.QuestionCreate
{
    public class QuestionCreateCommandHandler
    {
        private readonly IQuestionRepository<QuestionEntity> _QuestionRepository;
        private readonly IQuestionRepository<InfoQuestionEntity> _InfoQuestionRepository;

        

        public QuestionCreateCommandHandler(IQuestionRepository<QuestionEntity> questionRepository, IQuestionRepository<InfoQuestionEntity> infoQuestionRepository)
        {
            this._QuestionRepository = questionRepository;
            this._InfoQuestionRepository = infoQuestionRepository;
        }

        public async Task<QuestionCreateOutputCommand> HandleAsync(QuestionCreateInputCommand command)
        {

            var validator = new QuestionCreateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (!await _InfoQuestionRepository.ExistById(command.IdInfoQuestion))
            {
                throw new EntityNotFoundException("El contexto de la pregunta no existe");
            }

            var question = new QuestionEntity(enunciated: command.Enunciated, feedback: command.Feedback, optionType: command.OptionType, optionA: command.OptionA, optionB: command.OptionB, optionC: command.OptionC, optionD: command.OptionD, correctAnswer: command.CorrectAnswer, idInfoQuestion: command.IdInfoQuestion, typeQuestion: command.TypeQuestion);

            var resp = await this._QuestionRepository.Add(question);

            return new QuestionCreateOutputCommand(enunciated: resp.Enunciated, feedback: resp.Feedback, optionType: resp.OptionType, optionA: resp.OptionA, optionB: resp.OptionB, optionC: resp.OptionC, optionD: resp.OptionD, correctAnswer: resp.CorrectAnswer, infoQuestion: resp.InfoQuestion, typeQuestion: resp.TypeQuestion, id: resp.Id);
        }
    }
}

