using e_commerceUserServiceBackend.DTO;
using e_commerceUserServiceBackend.Models;

namespace e_commerceUserServiceBackend.Services
{
    public interface IUserService
    {
        Task <ApiResponseDTO?> UserSignUp(User user);
        Task<ApiResponseDTO?> UserLogin(UserDTO user);
        Task<List<User>> GetAllUsers();
    }
}
