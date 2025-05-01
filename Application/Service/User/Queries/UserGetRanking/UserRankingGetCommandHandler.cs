
using Application.Base.Validate;
using Application.Common.Exceptions;
using Domain.Entity;
using Domain.Entity.RankingResponseEntity;
using Domain.Port.User;

namespace Application.Service.User.Commands.UserUpdate
{
    public class UserRankingGetCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public UserRankingGetCommandHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<RankingResponseEntity<UserEntity>> HandleAsync(string userId)
        {

            return await this._userRepository.GetRankingByScore(userId);
        }


    }
}

