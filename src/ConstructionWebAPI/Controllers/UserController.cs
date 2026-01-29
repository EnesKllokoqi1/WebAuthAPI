using ConstructionWebAPI.DTOS.AssignmentDTOS;
using ConstructionWebAPI.DTOS.BuildingDTOS;
using ConstructionWebAPI.DTOS.UserDTOS;
using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

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
        public async Task<ActionResult<UserResponseDTO?>> GetUserData()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
            var parsedId = Guid.Parse(userIdClaim);
            var user = await _userService.GetUserData(parsedId);
            if (user is null)
            {
                return NotFound($"User with Id: {parsedId} has not been found");
            }
            return Ok(user);
        }
        [Authorize]
        [HttpGet("see-user-buildings")]
        public async Task<ActionResult<List<BuildingWithOwnerResponseDTO>>> SeeUserBuildings()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
             var parsedId = Guid.Parse(userIdClaim);
            var buildings = await _userService.SeeUserBuildings(parsedId);
            return Ok(new
            {
                message = buildings.Count == 0 ? "No buildings are yet to be created for the user " : null,
                data = buildings
            });
        }
        [Authorize]
        [HttpGet("see-user-assignments")]
        public async Task<ActionResult<List<AssignmentsWithUserResponseDTO>>> SeeUserAssignments()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
            var parsedId = Guid.Parse(userIdClaim);
            var assignments = await _userService.SeeUserAssignments(parsedId);
            return Ok(new
            {
                message = assignments.Count == 0 ? "No assignments are yet to be created for the user " : null,
                data = assignments
            });
        }
        [Authorize]
        [HttpPut("update-user-data")]
        public async Task<ActionResult<UserResponseDTO?>> UpdateUserData(UserRegisterDTO userRegisterDTO)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
            var parsedId = Guid.Parse(userIdClaim);
            var userdata = await _userService.UpdateUserData(parsedId,userRegisterDTO);
            if (userdata is null)
            {
                return NotFound($"User with Id: {parsedId} has not been found");
            }
            return Ok(userdata);
        }
        [Authorize]
        [HttpDelete("delete-user-account")]
        public async Task<ActionResult<UserResponseDTO?>> DeleteUserAccount()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
            var parsedId = Guid.Parse(userIdClaim);
            var userdata = await _userService.DeleteAccount(parsedId);
            if (userdata is false)
            {
                return NotFound($"User with Id: {parsedId} has not been found");
            }
            return NoContent();
        }
    }
}
