using ConstructionWebAPI.DTOS.UserDTOS;
using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        [HttpGet("get-user-data")]
        public async Task<ActionResult> GetUserData()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
            var parsedId = Guid.Parse(userIdClaim);
            var user = await _userService.GetUserData(parsedId);
            return Ok(user);
        }
        [Authorize]
        [HttpGet("see-users-buildings")]
        public async Task<ActionResult> SeeUsersBuildings()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
             var parsedId = Guid.Parse(userIdClaim);
            var buildings = await _userService.SeeUsersBuildings(parsedId);
            return Ok(new
            {
                message = buildings.Count == 0 ? "No buildings are yet to be created for the user " : null,
                data = buildings
            });
        }

        [Authorize]
        [HttpPut("update-users-data")]
        public async Task<ActionResult> UpdateUsersData(UserRegisterDTO userRegisterDTO)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
            var parsedId = Guid.Parse(userIdClaim);
            var userdata = await _userService.UpdateUserData(parsedId,userRegisterDTO);
            if (userdata is null)
            {
                return NotFound($"User with ID: {parsedId} has not been found");
            }
            return Ok(userdata);
        }
    }
}
