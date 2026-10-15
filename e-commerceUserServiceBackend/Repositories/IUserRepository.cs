using e_commerceUserServiceBackend.DTO;
using e_commerceUserServiceBackend.Models;

namespace e_commerceUserServiceBackend.Repositories
{
    public interface IUserRepository
    {
        Task<ApiResponseDTO?> UserSignUp(User user);
        Task<ApiResponseDTO?> UserLogin(UserDTO user);
        Task<List<User>> GetAllUsers();

    }
}
