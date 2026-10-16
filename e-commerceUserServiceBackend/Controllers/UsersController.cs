using e_commerceUserServiceBackend.DTO;
using e_commerceUserServiceBackend.Models;
using e_commerceUserServiceBackend.Services;
using Microsoft.AspNetCore.Authorization;
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


        //[HttpPost("register")]
        //public async Task<IActionResult> userSignUp(User user)
        //{
        //    var msg = await _userService.UserSignUp(user);
        //    if (msg == "User Registerd Successfully")
        //        return Ok(new { Message = msg });

        //    return BadRequest(new { Message = msg });
        //    //return Ok(new { Message = "User Registered!" });
        //}

        //after token

        [HttpPost ("register")]
        public async Task<IActionResult> userSignUp(User user)
        {
            var res = await _userService.UserSignUp(user);
            if (!res.Successs)
               return BadRequest(res);
            
            return Ok( res);
            //return Ok(new { Message = "User Registered!" });
        }

        //public async Task<IActionResult> userLogin(UserDTO user)
        //{
        //    var msg = await _userService.UserLogin(user);
        //    if (msg == null)
        //        return NotFound(new { message = "User not found" });
        //    if (msg == "User Logged in successfully")
        //        return Ok(new { Message = msg });
        //    return BadRequest(new { Message = msg });
        //}

        //after token

        [HttpPost("login")]

        public async Task<IActionResult> userLogin(UserDTO user)
        {
            var res =await _userService.UserLogin(user);
            if (!res.Successs)
                return BadRequest(res);

            return Ok(res);
        }

        [Authorize]
        [HttpGet("get-users")]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
    }
        
    
}
