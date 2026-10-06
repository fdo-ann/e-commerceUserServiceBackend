using e_commerceUserServiceBackend.Models;

namespace e_commerceUserServiceBackend.Repositories
{
    public interface IUserRepository
    {
        Task<string?> UserSignUp(User user);
    }
}
