
using Application.Common.Exceptions;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionUpdate
{
    public class InfoQuestionUpdateCommandHandler
    {
        private readonly IQuestionRepository<InfoQuestionEntity> _InfoQuestionRepository;

        public InfoQuestionUpdateCommandHandler(IQuestionRepository<InfoQuestionEntity> InfoQuestionRepository)
        {
            this._InfoQuestionRepository = InfoQuestionRepository;
        }

        public async Task<bool> HandleAsync(InfoQuestionUpdateInputCommand command)
        {

            var validator = new InfoQuestionUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var InfoQuestion = new InfoQuestionEntity(image: command.Image, context: command.Context, idCompetence: command.IdCompetence);

            InfoQuestion.Id = command.Id;
            InfoQuestion.DateUpdate = DateTime.Now;


            if (!await _InfoQuestionRepository.ExistById(InfoQuestion.Id))
            {
                throw new EntityNotFoundException("La informacion de preguntas a actualizar no existe");
            }

            return await this._InfoQuestionRepository.Update(InfoQuestion);
        }
    }
}

