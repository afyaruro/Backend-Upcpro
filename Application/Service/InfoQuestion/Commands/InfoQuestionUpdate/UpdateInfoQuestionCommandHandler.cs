
using Application.Common.Exceptions;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionUpdate
{
    public class UpdateInfoQuestionCommandHandler
    {
        private readonly IQuestionRepository<InfoQuestionEntity> _InfoQuestionRepository;

        public UpdateInfoQuestionCommandHandler(IQuestionRepository<InfoQuestionEntity> InfoQuestionRepository)
        {
            this._InfoQuestionRepository = InfoQuestionRepository;
        }

        public async Task<bool> HandleAsync(UpdateInfoQuestionCommand command)
        {

            var validator = new UpdateInfoQuestionCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var InfoQuestion = new InfoQuestionEntity();


            InfoQuestion.Id = command.Id;
            InfoQuestion.Contexto = command.Contexto;
            InfoQuestion.DateUpdate = DateTime.Now;
            InfoQuestion.Fuente = command.Fuente;
            InfoQuestion.TypeQuestion = command.TypeQuestion;


            if (!await _InfoQuestionRepository.ExistById(InfoQuestion.Id))
            {
                throw new EntityNotFoundException("La informacion de preguntas a actualizar no existe");
            }



            return await this._InfoQuestionRepository.Update(InfoQuestion);
        }
    }
}

