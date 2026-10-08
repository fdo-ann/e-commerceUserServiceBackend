using e_commerceUserServiceBackend.DTO;
using e_commerceUserServiceBackend.Models;
using e_commerceUserServiceBackend.Repositories;

namespace e_commerceUserServiceBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string?> UserLogin(UserDTO user)
        {
            var msg = await _userRepository.UserLogin(user);
            return msg;
        }

        public async Task<string?> UserSignUp( User user)
        {
            var msg = await _userRepository.UserSignUp(user);
            return msg;
        }
    }
}
