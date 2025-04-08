using Domain.Base.ResponseEntity;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.Question.Commands.QuestionGetAllPage
{
    public class QuestionGetAllPageCommandHandler
    {
        private readonly IQuestionRepository<QuestionEntity> _QuestionRepository;

        public QuestionGetAllPageCommandHandler(IQuestionRepository<QuestionEntity> QuestionRepository)
        {
            this._QuestionRepository = QuestionRepository;
        }

        public async Task<ResponseEntity<QuestionGetAllPageOutputCommand>> HandleAsync(QuestionGetAllPageInputCommand command)
        {

            var validator = new QuestionGetAllPageCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._QuestionRepository.GetAll(page: command.Page, pageSize: command.Size));
        }

        private ResponseEntity<QuestionGetAllPageOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<QuestionEntity> resp)
        {
            var responseCommands = new ResponseEntity<QuestionGetAllPageOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<QuestionGetAllPageOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var command = new QuestionGetAllPageOutputCommand(enunciated: entity.Enunciated, feedback: entity.Feedback, optionType: entity.OptionType, optionA: entity.OptionA, optionB: entity.OptionB, optionC: entity.OptionC, optionD: entity.OptionD, correctAnswer: entity.CorrectAnswer, infoQuestion: entity.InfoQuestion, typeQuestion: entity.TypeQuestion, id: entity.Id);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

