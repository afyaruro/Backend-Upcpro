
using Application.Service.Faculty.Commands.FacultyGetAllPage;
using Domain.Base.ResponseEntity;
using Domain.Entity.Program;
using Domain.Port.Program;
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramGetAllPage
{
    public class GetAllPageProgramCommandHandler
    {
        private readonly IProgramRepository _ProgramRepository;

        public GetAllPageProgramCommandHandler(IProgramRepository ProgramRepository)
        {
            this._ProgramRepository = ProgramRepository;
        }

        public async Task<ResponseEntity<GetAllPageProgramOutputCommand>> HandleAsync(GetAllPageProgramInputCommand command)
        {

            var validator = new GetAllPageProgramCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._ProgramRepository.GetAll(page: command.Page, size: command.Size));
        }

        private ResponseEntity<GetAllPageProgramOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<ProgramEntity> resp)
        {
            var responseCommands = new ResponseEntity<GetAllPageProgramOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<GetAllPageProgramOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var facultyCommand = entity.Faculty != null ? new GetAllPageFacultyOutputCommand(name: entity.Faculty.Name, id: entity.Faculty.Id) : null;
                var command = new GetAllPageProgramOutputCommand(name: entity.Name, id: entity.Id, faculty: facultyCommand!);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

