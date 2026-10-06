using e_commerceUserServiceBackend.Models;

namespace e_commerceUserServiceBackend.Services
{
    public interface IUserService
    {
        Task <string?> UserSignUp(User user);
    }
}
