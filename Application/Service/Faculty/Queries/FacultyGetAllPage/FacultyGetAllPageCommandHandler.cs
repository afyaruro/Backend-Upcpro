
using Domain.Base.ResponseEntity;
using Domain.Entity.Facultad;
using Domain.Port.Faculty;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class FacultyGetAllPageCommandHandler
    {
        private readonly IFacultyRepository _FacultyRepository;

        public FacultyGetAllPageCommandHandler(IFacultyRepository FacultyRepository)
        {
            this._FacultyRepository = FacultyRepository;
        }

        public async Task<ResponseEntity<FacultyGetAllPageOutputCommand>> HandleAsync(FacultyGetAllPageInputCommand command)
        {

            var validator = new FacultyGetAllPageCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._FacultyRepository.GetAll(page: command.Page, size: command.Size));
        }

        private ResponseEntity<FacultyGetAllPageOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<FacultyEntity> resp)
        {
            var responseCommands = new ResponseEntity<FacultyGetAllPageOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<FacultyGetAllPageOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var command = new FacultyGetAllPageOutputCommand(name: entity.Name, id: entity.Id, dateUpdate: entity.DateUpdate);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

