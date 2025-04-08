using Domain.Base.ResponseEntity;
using Domain.Entity;
using Domain.Port.User;
using FluentValidation;

namespace Application.Service.User.Commands.UserGetAllPage
{
    public class UserGetAllPageCommandHandler
    {
        private readonly IUserRepository _UserRepository;

        public UserGetAllPageCommandHandler(IUserRepository UserRepository)
        {
            this._UserRepository = UserRepository;
        }

        public async Task<ResponseEntity<UserGetAllPageOutputCommand>> HandleAsync(UserGetAllPageInputCommand command)
        {

            var validator = new UserGetAllPageCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._UserRepository.GetAll(page: command.Page, pageSize: command.Size));
        }

        private ResponseEntity<UserGetAllPageOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<UserEntity> resp)
        {
            var responseCommands = new ResponseEntity<UserGetAllPageOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<UserGetAllPageOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var command = new UserGetAllPageOutputCommand();
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

