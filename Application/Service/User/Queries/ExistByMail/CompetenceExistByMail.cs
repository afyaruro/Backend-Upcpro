using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Port.User;

namespace Application.Service.User.Queries.ExistByMail
{
    public class CompetenceExistByMail
    {
        private readonly IUserRepository _userRepository;

        public CompetenceExistByMail(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<bool> QueryAsync(string mail)
        {
            if (await this._userRepository.GetByMail(mail) != null)
            {
                return true;
            }
            return false;
        }
    }
}




