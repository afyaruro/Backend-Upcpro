
using Application.Service.Faculty.Commands.FacultyGetAllPage;
using Domain.Base.ResponseEntity;
using Domain.Entity.Program;
using Domain.Port.Program;
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramGetAllPage
{
    public class ProgramGetAllPageSyncCommandHandler
    {
        private readonly IProgramRepository _ProgramRepository;

        public ProgramGetAllPageSyncCommandHandler(IProgramRepository ProgramRepository)
        {
            this._ProgramRepository = ProgramRepository;
        }

        public async Task<ResponseEntity<ProgramGetAllPageOutputCommand>> HandleAsync(ProgramGetAllPageSyncInputCommand command)
        {

            var validator = new ProgramGetAllPageSyncCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._ProgramRepository.GetAll(command.LateDateSync));
        }

        private ResponseEntity<ProgramGetAllPageOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<ProgramEntity> resp)
        {
            var responseCommands = new ResponseEntity<ProgramGetAllPageOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<ProgramGetAllPageOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var facultyCommand = entity.Faculty != null ? new FacultyGetAllPageOutputCommand(name: entity.Faculty.Name, id: entity.Faculty.Id, dateUpdate: entity.Faculty.DateUpdate, espacioFisicoId: entity.Faculty.SedeId) : null;
                var command = new ProgramGetAllPageOutputCommand(name: entity.Name, id: entity.Id, faculty: facultyCommand!, dateUpdate: entity.DateUpdate);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

