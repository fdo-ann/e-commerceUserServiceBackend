using e_commerceUserServiceBackend.DTO;
using e_commerceUserServiceBackend.Models;

namespace e_commerceUserServiceBackend.Services
{
    public interface IUserService
    {
        Task <string?> UserSignUp(User user);
        Task<string?> UserLogin(UserDTO user);
    }
}
