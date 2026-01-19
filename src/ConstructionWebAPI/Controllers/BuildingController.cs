using ConstructionWebAPI.DTOS.BuildingDTOS;
using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks.Dataflow;

namespace ConstructionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        public IBuildingService _buildingService;
        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }
        [AllowAnonymous]
        [HttpGet("get-all-building-types")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllBuildingTypes()
        {
            var allBuildingTypes = await _buildingService.GetAllBuildingTypes();
            return Ok(new
            {
                BuildingTypes= allBuildingTypes
            });
        }
        [Authorize]
        [HttpDelete("delete-building-id/{id:guid}")]
        public async Task<ActionResult<bool>> DeleteBuilding(Guid id)
        {
            var check = await _buildingService.DeleteBuilding(id);
            if (check!)
            {
                return NotFound($"Building with Id : {id} not found");
            }
            return NoContent();
        }
        [Authorize]
        [HttpPost("purchase-building")]
        public async Task<ActionResult<Building?>> PurchaseBuilding(BuildingDTO buildingDTO)
        {
            var building = await _buildingService.PurchaseBuilding(buildingDTO);
            if (building is null)
            {
                return Conflict($"Building has already been bought");
            }
            return Ok(building);
        }
        [Authorize]
        [HttpPut("update-buildings-data-buildingId/{buildingId:guid}")]
        public async Task<ActionResult<Building?>> UpdateBuildingData(Guid buildingId,BuildingDTO buildingDTO)
        {
            var building = await _buildingService.UpdateBuildingData(buildingId,buildingDTO);
            if (building is null)
            {
                return NotFound($"Building with Id:{buildingId} not found");
            }
            return Ok(building);

        }
        [Authorize]
        [HttpPost("connect-user-with-building")]
        public async Task<ActionResult<Building?>> ConnectUserWithBuilding(UserBuildingConnectionDTO userBuildingConnectionDTO)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
            var parsedId = Guid.Parse(userIdClaim);
            var building = await _buildingService.ConnectUserWithBuilding(userBuildingConnectionDTO.EmailAddress,parsedId,userBuildingConnectionDTO.BuildingId);
            if (building is null)
            {
                return NotFound("User, building, or email address is not found.");
            }
            return Ok(building);
        }

    }
}
