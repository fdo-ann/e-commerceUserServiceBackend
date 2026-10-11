using e_commerceUserServiceBackend.DTO;
using e_commerceUserServiceBackend.Models;
using e_commerceUserServiceBackend.Repositories;
using System.Text;
using System.Text.RegularExpressions;

namespace e_commerceUserServiceBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string?> UserLogin(UserDTO user)
        {
            var msg = await _userRepository.UserLogin(user);
            return msg;
        }

        public async Task<string?> UserSignUp( User user)
        {
            var passMessage = CheckPasswordStrength(user.Password);
            if (!string.IsNullOrEmpty(passMessage))
                return passMessage;
            var msg = await _userRepository.UserSignUp(user);
            return msg;
        }

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 7)
                sb.Append("Minimum password length should be 7" + Environment.NewLine);

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be alphanumeric" + Environment.NewLine);

            if (!Regex.IsMatch(password, @"[<>,@!#$%^&*()_+={}\[\]:;""'|\\?/~`-]"))
            {
                sb.Append("Password should contain special chars" + Environment.NewLine);
            }

            return sb.ToString();
        }

    }
}
