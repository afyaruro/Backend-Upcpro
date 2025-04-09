
using Application.Common.Exceptions;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionCreate
{
    public class InfoQuestionCreateCommandHandler
    {
        private readonly IQuestionRepository<InfoQuestionEntity> _InfoQuestionRepository;

        public InfoQuestionCreateCommandHandler(IQuestionRepository<InfoQuestionEntity> InfoQuestionRepository)
        {
            this._InfoQuestionRepository = InfoQuestionRepository;
        }

        public async Task<InfoQuestionCreateOutputCommand> HandleAsync(InfoQuestionCreateInputCommand command)
        {

            var validator = new InfoQuestionCreateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var infoQuestion = new InfoQuestionEntity(context: command.Context, image: command.Image, idCompetence: command.IdCompetence);

            var resp = await this._InfoQuestionRepository.Add(infoQuestion);

            return new InfoQuestionCreateOutputCommand(context: resp.Context, image: resp.Image, id: resp.Id, idCompetence: resp.IdCompetence);
        }
    }
}

