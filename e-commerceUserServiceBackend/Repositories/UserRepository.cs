using e_commerceUserServiceBackend.Context;
using e_commerceUserServiceBackend.DTO;
using e_commerceUserServiceBackend.Helpers;
using e_commerceUserServiceBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
            user.Password = PasswordHasher.HashPassword(user.Password);
            // checks userName already exists
            if (await CheckUserNameExistAsync(user.Username))
                return "UserName already exists";
            if (await CheckUserEmailExistAsync(user.Email))
                return "User email already exists";
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            //return user;
            return "User Registerd Successfully";
        }

        //private async Task<bool> CheckUserNameExistAsync(string userName)
        //{
        //    var userNameExist = await _dbContext.Users.AnyAsync(x=>x.Username == userName);
        //    return userNameExist;
        //}

        private async Task<bool> CheckUserNameExistAsync(string userName) =>
            await _dbContext.Users.AnyAsync(x => x.Username == userName);
        private async Task<bool> CheckUserEmailExistAsync(string email) =>
           await _dbContext.Users.AnyAsync(x => x.Email == email);
    }
}
