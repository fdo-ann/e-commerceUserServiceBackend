using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerceUserServiceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("usersSample")]
        public async Task<IActionResult> getSample()
        {
            return Ok("Hello from backEnd");
        }
    }
}
