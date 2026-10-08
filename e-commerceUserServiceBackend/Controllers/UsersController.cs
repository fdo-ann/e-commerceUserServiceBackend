using e_commerceUserServiceBackend.DTO;
using e_commerceUserServiceBackend.Models;
using e_commerceUserServiceBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerceUserServiceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("usersSample")]
        public async Task<IActionResult> getSample()
        {
            return Ok("Hello from backEnd");
        }

        [HttpPost ("register")]
        public async Task<IActionResult> userSignUp(User user)
        {
            var msg = await _userService.UserSignUp(user);
            return Ok( new { Message =msg});
            //return Ok(new { Message = "User Registered!" });
        }

        [HttpPost("login")]

        public async Task<IActionResult> userLogin(UserDTO user)
        {
            var msg =await _userService.UserLogin(user);
            if (msg == null)
                return NotFound(new { message = "User not found" });

            return Ok(msg);
        }
    }
        
    
}
