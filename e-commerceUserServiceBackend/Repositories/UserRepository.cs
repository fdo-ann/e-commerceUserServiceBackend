using e_commerceUserServiceBackend.Context;
using e_commerceUserServiceBackend.DTO;
using e_commerceUserServiceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerceUserServiceBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _dbContext;
        public UserRepository( AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string?> UserLogin(UserDTO user)
        {
            var users = await _dbContext.Users.FirstOrDefaultAsync(x=> x.Username == user.Username && x.Password == user.Password);
            if (users == null)
                return null;
            return "User Logged in successfully";
        }

        public async Task<string> UserSignUp( User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            //return user;
            return "User Registerd Sucessfully";
        }
    }
}
