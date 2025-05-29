
using Domain.Base.ResponseEntity;
using Domain.Entity.Facultad;
using Domain.Port.Faculty;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class FacultyGetAllPageSyncCommandHandler
    {
        private readonly IFacultyRepository _FacultyRepository;

        public FacultyGetAllPageSyncCommandHandler(IFacultyRepository FacultyRepository)
        {
            this._FacultyRepository = FacultyRepository;
        }

        public async Task<ResponseEntity<FacultyGetAllPageOutputCommand>> HandleAsync(FacultyGetAllPageSyncInputCommand command)
        {

            var validator = new FacultyGetAllPageSyncCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._FacultyRepository.GetAll(command.LateDateSync));
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
                var command = new FacultyGetAllPageOutputCommand(name: entity.Name, id: entity.Id, dateUpdate: entity.DateUpdate, espacioFisicoId: entity.SedeId);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

