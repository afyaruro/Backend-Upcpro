using Domain.Base.ResponseEntity;
using Domain.Entity;
using Domain.Port.User;
using FluentValidation;

namespace Application.Service.User.Commands.UserGetAllPage
{
    public class GetAllPageUserCommandHandler
    {
        private readonly IUserRepository _UserRepository;

        public GetAllPageUserCommandHandler(IUserRepository UserRepository)
        {
            this._UserRepository = UserRepository;
        }

        public async Task<ResponseEntity<GetAllPageUserOutputCommand>> HandleAsync(GetAllPageUserInputCommand command)
        {

            var validator = new GetAllPageUserCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._UserRepository.GetAll(page: command.Page, pageSize: command.Size));
        }

        private ResponseEntity<GetAllPageUserOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<UserEntity> resp)
        {
            var responseCommands = new ResponseEntity<GetAllPageUserOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<GetAllPageUserOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var command = new GetAllPageUserOutputCommand();
                command.Id = entity.Id;
                command.Mail = entity.Mail;
                command.Image = entity.Image;
                command.FirstName = entity.FirstName;
                command.LastName = entity.LastName;
                command.Gender = entity.Gender;
                command.TypeIdentification = entity.TypeIdentification;
                command.Identification = entity.Identification;
                command.TypeUser = entity.TypeUser;
                command.Program = entity.Program;
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

