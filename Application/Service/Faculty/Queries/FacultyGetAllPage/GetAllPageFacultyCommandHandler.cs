
using Domain.Base.ResponseEntity;
using Domain.Entity.Facultad;
using Domain.Port.Faculty;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyGetAllPage
{
    public class GetAllPageFacultyCommandHandler
    {
        private readonly IFacultyRepository _FacultyRepository;

        public GetAllPageFacultyCommandHandler(IFacultyRepository FacultyRepository)
        {
            this._FacultyRepository = FacultyRepository;
        }

        public async Task<ResponseEntity<GetAllPageFacultyOutputCommand>> HandleAsync(GetAllPageFacultyInputCommand command)
        {

            var validator = new GetAllPageFacultyCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._FacultyRepository.GetAll(page: command.Page, size: command.Size));
        }

        private ResponseEntity<GetAllPageFacultyOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<FacultyEntity> resp)
        {
            var responseCommands = new ResponseEntity<GetAllPageFacultyOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<GetAllPageFacultyOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var command = new GetAllPageFacultyOutputCommand(name: entity.Name, id: entity.Id);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

