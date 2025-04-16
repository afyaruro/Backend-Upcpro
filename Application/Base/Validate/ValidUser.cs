
using Application.Service.User;

namespace Application.Base.Validate
{
    public static class ValidUser
    {
        public static async Task<bool> IsUserAccess(string userId, UserService _userService, string typeUser){

            

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return false;
                }

                var existUser = await _userService.IsUserType(type: typeUser, userId: userId!);

                if (existUser == null)
                {
                    return false;
                }

                return true;
            
        }
    }
}