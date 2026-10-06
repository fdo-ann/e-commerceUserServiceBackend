using e_commerceUserServiceBackend.Context;
using e_commerceUserServiceBackend.Models;

namespace e_commerceUserServiceBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _dbContext;
        public UserRepository( AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
