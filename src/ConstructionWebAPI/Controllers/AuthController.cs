using ConstructionWebAPI.Data;
using ConstructionWebAPI.DTOS;
using ConstructionWebAPI.DTOS.UserDTOS;
using ConstructionWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConstructionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _authService;
        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost("register")] 
        public async Task<ActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            var user = await _authService.RegisterAsync(userRegisterDTO);
            if(user is null)
            {
                return BadRequest($"User with the email {userRegisterDTO.Email} already exists.");
            }
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult> LogIn(UserLoginDTO userLoginDTO)
        {
            var user = await _authService.LogInAsync(userLoginDTO);
            if(user is null)
            {
                return Unauthorized(new { error = "Invalid email or password" });
            }
            return Ok(user);
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(RefreshTokenRequestDTO refreshTokenRequestDTO)
        {
            var refreshToken = await _authService.RefreshToken(refreshTokenRequestDTO);
            if (refreshToken is null)
            {
                return BadRequest("Invalid refresh token");
            }
            return Ok(refreshToken);
        }

        
    }
}
