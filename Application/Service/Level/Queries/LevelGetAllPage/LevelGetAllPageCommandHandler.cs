
using Domain.Base.ResponseEntity;
using Domain.Entity.Level;
using Domain.Port.Level;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class LevelGetAllPageCommandHandler
    {
        private readonly ILevelRepository _LevelRepository;

        public LevelGetAllPageCommandHandler(ILevelRepository LevelRepository)
        {
            this._LevelRepository = LevelRepository;
        }

        public async Task<ResponseEntity<LevelGetAllPageOutputCommand>> HandleAsync(LevelGetAllPageInputCommand command)
        {

            var validator = new LevelGetAllPageCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._LevelRepository.GetAll(page: command.Page, pageSize: command.Size));
        }

        private ResponseEntity<LevelGetAllPageOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<LevelEntity> resp)
        {
            var responseCommands = new ResponseEntity<LevelGetAllPageOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<LevelGetAllPageOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var command = new LevelGetAllPageOutputCommand(level: entity.Level, id: entity.Id, reward: entity.Reward, dificulty: entity.Dificulty, numQuestion: entity.NumQuestion);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

