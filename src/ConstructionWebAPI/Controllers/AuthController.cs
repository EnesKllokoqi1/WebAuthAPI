        using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost("register")] 
        public ActionResult Register()
        {
            return Ok();
        }
        [HttpPost("login")]
        public ActionResult LogIn()
        {
            return Ok();
        }
        [HttpGet("refresh-token")]
        public ActionResult RefreshToken()
        {
            return Ok();
        }

        
    }
}
