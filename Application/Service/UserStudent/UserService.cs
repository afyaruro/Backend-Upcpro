using Application.Service.User.Commands.User;
using Application.Service.User.Commands.UserCreate;
using Application.Service.User.Commands.UserGetAllPage;
using Application.Service.User.Commands.UserLogin;
using Application.Service.User.Commands.UserUpdate;
using Domain.Base.ResponseEntity;
using Domain.Port.User;

namespace Application.Service.User
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) => _repository = repository;

        public async Task<CreateOutputUserCommand> Create(CreateInputUserCommand command)
        {
            var _create = new CreateUserCommandHandler(_repository);
            return await _create.HandleAsync(command, "student");
        }

        public async Task<CreateOutputUserCommand> CreateAdmin(CreateInputUserCommand command)
        {
            var _create = new CreateUserCommandHandler(_repository);
            return await _create.HandleAsync(command, "admin");
        }

        public async Task<CreateOutputUserCommand> CreateCreator(CreateInputUserCommand command)
        {
            var _create = new CreateUserCommandHandler(_repository);
            return await _create.HandleAsync(command, "creator");
        }

        public async Task<ResponseEntity<GetAllPageUserOutputCommand>> GetAllPage(GetAllPageUserInputCommand command)
        {
            var _getAll = new GetAllPageUserCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> UpdateForAdmin(UpdateUserForAdminCommand command)
        {
            var _update = new UpdateUserCommandHandler(_repository);
            return await _update.HandleAsync(command, command.Id);
        }

        public async Task<bool> Update(UpdateUserCommand command, string userId)
        {
            var _update = new UpdateUserCommandHandler(_repository);
            return await _update.HandleAsync(command, userId);
        }

        public async Task<bool> UpdateMailForAdmin(UpdateUserMailForAdminCommand command)
        {
            var _update = new UpdateUserMailCommandHandler(_repository);
            return await _update.HandleAsync(command, command.Id);
        }

        public async Task<bool> UpdateMail(UpdateUserMailCommand command, string userId)
        {
            var _update = new UpdateUserMailCommandHandler(_repository);
            return await _update.HandleAsync(command, userId);
        }

        public async Task<bool> UpdatePasswordForAdmin(UpdateUserPasswordForAdminCommand command)
        {
            var _update = new UpdateUserPasswordCommandHandler(_repository);
            return await _update.HandleAsync(command, command.Id);
        }

        public async Task<bool> UpdatePassword(UpdateUserPasswordCommand command, string userId)
        {
            var _update = new UpdateUserPasswordCommandHandler(_repository);
            return await _update.HandleAsync(command, userId);
        }

        public async Task<ResponseEntity<CreateOutputUserCommand>> Login(UserLoginCommand command)
        {
            var _update = new UserLoginCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<bool> ExistById(string userId)
        {

            return await _repository.ExistById(userId);
        }

        public async Task<bool> IsUserType(string type, string userId)
        {

            return await _repository.IsUserType(userId, type);
        }




    }
}