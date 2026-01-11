using ConstructionWebAPI.DTOS;
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
        [HttpGet("get-user-data/{id:guid}")]
        public async Task<ActionResult> GetUserData(Guid id)
        {
            var user = await _userService.GetUserData(id);
            return Ok(user);
        }
        [Authorize]
        [HttpGet("see-users-buildings/{id:guid}")]
        public async Task<ActionResult> SeeUsersBuildings(Guid id)
        {
            var buildings = await _userService.SeeUsersBuildings(id);
            return Ok(new
            {
                message = buildings.Count == 0 ? "No buildings are yet to be created for the user " : null,
                data = buildings
            });
        }

        [Authorize]
        [HttpPut("update-users-data/{guid:guid}")]
        public async Task<ActionResult> UpdateUsersData(Guid guid,UserRegisterDTO userRegisterDTO)
        {
            var userdata = await _userService.UpdateUserData(guid,userRegisterDTO);
            if (userdata is null)
            {
                return NotFound($"User with ID: {guid} has not been found");
            }
            return Ok(userdata);
        }
    }
}
