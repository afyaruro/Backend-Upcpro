using Application.Service.User.Commands.User;
using Application.Service.User.Commands.UserCreate;
using Application.Service.User.Commands.UserGetAllPage;
using Application.Service.User.Commands.UserLogin;
using Application.Service.User.Commands.UserUpdate;
using Domain.Base.ResponseEntity;
using Domain.Entity;
using Domain.Entity.RankingResponseEntity;
using Domain.Port.User;

namespace Application.Service.User
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) => _repository = repository;

        public async Task<UserCreateOutputCommand> Create(UserCreateInputCommand command)
        {
            var _create = new UserCreateCommandHandler(_repository);
            return await _create.HandleAsync(command, "student");
        }

        public async Task<UserCreateOutputCommand> CreateForAdmin(UserCreateInputCommand command, string typeUser)
        {
            var _create = new UserCreateCommandHandler(_repository);
            return await _create.HandleAsync(command, typeUser);
        }

        public async Task<ResponseEntity<UserGetAllPageOutputCommand>> GetAllPage(UserGetAllPageInputCommand command)
        {
            var _getAll = new UserGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> UpdateForAdmin(UserForAdminUpdateInputCommand command)
        {
            var _update = new UserUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command, command.Id);
        }

        public async Task<bool> Update(UserUpdateInputCommand command, string userId)
        {
            var _update = new UserUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command, userId);
        }

        public async Task<bool> UpdateMailForAdmin(UserMailUpdateForAdminInputCommand command)
        {
            var _update = new UserMailUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command, command.Id);
        }

        public async Task<bool> UpdateMail(UserMailUpdateInputCommand command, string userId)
        {
            var _update = new UserMailUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command, userId);
        }

        public async Task<bool> UpdatePasswordForAdmin(UserPasswordForAdminUpdateInputCommand command)
        {
            var _update = new UserPasswordUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command, command.Id);
        }

        public async Task<bool> UpdatePassword(UserPasswordUpdateInputCommand command, string userId)
        {
            var _update = new UserPasswordUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command, userId);
        }

        public async Task<bool> UpdatePasswordByMail(UserPasswordByMailUpdateInputCommand command)
        {
            var _update = new UserPasswordByMailUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<ResponseEntity<UserCreateOutputCommand>> Login(UserLoginCommand command)
        {
            var _update = new UserLoginCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<bool> ExistById(string userId)
        {

            return await _repository.ExistById(userId);
        }

        public async Task<UserEntity?> IsUserType(string type, string userId)
        {
            return await _repository.IsUserType(type: type, id: userId);
        }

        public async Task<bool> UpdatePuntaje(UserPuntajeUpdateInputCommand command, string userId)
        {
            var _update = new UserPuntajeUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command, userId);
        }


        public async Task<RankingResponseEntity<UserEntity>> GetRanking(string idUser)
        {
            var _getAll = new UserRankingGetCommandHandler(_repository);
            return await _getAll.HandleAsync(idUser);
        }








    }
}