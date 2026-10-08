using e_commerceUserServiceBackend.DTO;
using e_commerceUserServiceBackend.Models;

namespace e_commerceUserServiceBackend.Repositories
{
    public interface IUserRepository
    {
        Task<string?> UserSignUp(User user);
        Task<string?> UserLogin(UserDTO user);
    }
}
