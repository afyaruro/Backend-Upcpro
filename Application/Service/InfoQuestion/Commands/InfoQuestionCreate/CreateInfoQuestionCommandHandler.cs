
using Application.Common.Exceptions;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionCreate
{
    public class CreateInfoQuestionCommandHandler
    {
        private readonly IQuestionRepository<InfoQuestionEntity> _InfoQuestionRepository;

        public CreateInfoQuestionCommandHandler(IQuestionRepository<InfoQuestionEntity> InfoQuestionRepository)
        {
            this._InfoQuestionRepository = InfoQuestionRepository;
        }

        public async Task<CreateOutputInfoQuestionCommand> HandleAsync(CreateInputInfoQuestionCommand command)
        {

            var validator = new CreateInfoQuestionCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var infoQuestion = new InfoQuestionEntity();
            infoQuestion.TypeQuestion = command.TypeQuestion;
            infoQuestion.DateUpdate = DateTime.Now;
            infoQuestion.Fuente = command.Fuente;
            infoQuestion.Contexto = command.Contexto;

            var resp = await this._InfoQuestionRepository.Add(infoQuestion);

            return new CreateOutputInfoQuestionCommand(context: resp.Contexto, fuente: resp.Fuente, typeQuestion: resp.TypeQuestion, id: resp.Id);
        }
    }
}

