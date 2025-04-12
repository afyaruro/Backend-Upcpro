using Domain.Base.ResponseEntity;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionGetAllPage
{
    public class InfoQuestionGetAllPageCommandHandler
    {
        private readonly IQuestionRepository<InfoQuestionEntity> _InfoQuestionRepository;

        public InfoQuestionGetAllPageCommandHandler(IQuestionRepository<InfoQuestionEntity> InfoQuestionRepository)
        {
            this._InfoQuestionRepository = InfoQuestionRepository;
        }

        public async Task<ResponseEntity<InfoQuestionGetAllPageOutputCommand>> HandleAsync(InfoQuestionGetAllPageInputCommand command)
        {

            var validator = new InfoQuestionGetAllPageCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._InfoQuestionRepository.GetAll(page: command.Page, pageSize: command.Size));
        }

        private ResponseEntity<InfoQuestionGetAllPageOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<InfoQuestionEntity> resp)
        {
            var responseCommands = new ResponseEntity<InfoQuestionGetAllPageOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<InfoQuestionGetAllPageOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var command = new InfoQuestionGetAllPageOutputCommand(context: entity.Context, id: entity.Id, image: entity.Image, dateUpdate: entity.DateUpdate);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

