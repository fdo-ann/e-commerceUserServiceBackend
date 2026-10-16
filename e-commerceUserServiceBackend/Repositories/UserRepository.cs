using e_commerceUserServiceBackend.Context;
using e_commerceUserServiceBackend.DTO;
using e_commerceUserServiceBackend.Helpers;
using e_commerceUserServiceBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_commerceUserServiceBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _dbContext;
        public UserRepository( AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<string?> UserLogin(UserDTO userObj)
        //{
        //    //var users = await _dbContext.Users.FirstOrDefaultAsync(x=> x.Username == user.Username && x.Password == user.Password);
        //    var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == userObj.Username);
        //    if (user == null)
        //        return null;
        //    if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
        //        return "Incorrect password entered";
        //    return "User Logged in successfully";
        //}

        //after token passing
        public async Task<ApiResponseDTO?> UserLogin(UserDTO userObj)
        {
            //var users = await _dbContext.Users.FirstOrDefaultAsync(x=> x.Username == user.Username && x.Password == user.Password);
            ApiResponseDTO res = new ApiResponseDTO();
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == userObj.Username);
            if (user == null) {
                res.Successs = false;
                return res;
            }
            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                res.Successs = false;
                res.Msg = "Incorrect password entered";
                return res;
            }
            // Generate JWT Token upon successful authentication
            user.Token = CreateJwtToken(user);

            res.Token = user.Token;
            res.Msg = "User Logged in successfully";

            return res;
        }

        //public async Task<string> UserSignUp( User user)
        //{
        //    user.Password = PasswordHasher.HashPassword(user.Password);
        //    // checks userName already exists
        //    if (await CheckUserNameExistAsync(user.Username))
        //        return "UserName already exists";
        //    if (await CheckUserEmailExistAsync(user.Email))
        //        return "User email already exists";
        //    _dbContext.Users.Add(user);
        //    await _dbContext.SaveChangesAsync();
        //    //return user;
        //    return "User Registerd Successfully";
        //}

        //after token passing
        public async Task<ApiResponseDTO> UserSignUp(User user)
        {
            ApiResponseDTO res = new ApiResponseDTO();
            user.Password = PasswordHasher.HashPassword(user.Password);
            // checks userName already exists
            if (await CheckUserNameExistAsync(user.Username))
            {
                res.Successs = false;
                res.Msg = "Username already exists";
                return res;
            }
            if (await CheckUserEmailExistAsync(user.Email))
            {
                res.Successs = false;
                res.Msg = "User email already exists";
                return res;
            }
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            //return user;
            res.Msg = "User Registerd Successfully";
            return res;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
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


        private string CreateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("MySuperSecretKeyForJwtToken123456!");

            //Token payload
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, user.Role ?? "User"),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            });
            //Token signature
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                //Expires = DateTime.Now.AddDays(1),
                Expires = DateTime.Now.AddSeconds(25),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
