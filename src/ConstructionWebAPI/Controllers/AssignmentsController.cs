using ConstructionWebAPI.DTOS.AssignmentDTOS;
using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConstructionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        public IAssignmentService _assignmentService;
        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }
        [Authorize]
        [HttpPost("create-assignment")] 
        public async Task<ActionResult<AssignmentResponseDTO>> CreateAssignment(AssignmentDTO assignmentDTO)
        {
            var assignment = await _assignmentService.CreateAssignment(assignmentDTO);
            if (assignment is null)
            {
                return Conflict($"Assignment already exists");
            }
            return CreatedAtAction(
          nameof(GetAssignmentDetails),
           new { guid = assignment.AssignmentId },
           assignment
          );

        }
        [Authorize]
        [HttpDelete("delete-assignment/{guid:guid}")]
        public async Task<ActionResult<bool>> DeleteAssignment(Guid guid)
        {
            var assignment = await _assignmentService.DeleteAssignment(guid);
            if (assignment is false)
            {
                return NotFound($"Assignment not found with ID : {guid}");
            }
            return NoContent();
        }
        [Authorize]
        [HttpGet("get-assignment-details/{guid:guid}")]
         public async Task<ActionResult<AssignmentResponseDTO>> GetAssignmentDetails(Guid guid)
        {
            var assignment = await _assignmentService.GetAssignmentDetails(guid);
            if (assignment is null)
            {
                return NotFound($"Assignment with ID : {guid} not found");
            }
            return Ok(assignment);
        }
        [Authorize]
        [HttpPut("update-assignment-data/{guid:guid}")]
        public async Task<ActionResult<AssignmentResponseDTO>> UpdateAssignmentData(Guid guid, AssignmentDTO dto)
        {
            var assignment = await _assignmentService.UpdateAssignmentData(guid,dto);
            if (assignment is null)
            {
                return NotFound($"Assignment with ID : {guid} not found");
            }
            return Ok(assignment);
        }
        [Authorize]
        [HttpPost("connect-user-to-assignment")]
        public async Task<ActionResult<AssignmentsWithUserResponseDTO>> ConnectUserToAssignment(AssignmentUserConnectionDTO assignmentUserConnectionDTO)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized();
            }
            var guid = Guid.Parse(userIdClaim);
            var assignment = await _assignmentService.ConnectUserToAssignment(guid,assignmentUserConnectionDTO);
            if (assignment is null)
            {
                return NotFound("Assignment or User not found");
            }
            return Ok(assignment);
        }
        [Authorize]
        [HttpPost("connect-building-to-assignment")]
        public async Task<ActionResult<AssignmentWithBuildingResponseDTO>> ConnectBuildingToAssignment(AssignmentBuildingConnectionDTO assignmentWithBuilding)
        {
            var assignment = await _assignmentService.ConnectBuildingToAssignment(assignmentWithBuilding);
            if (assignment is null)
            {
                return NotFound("Assignment or building not found");
            }
            return Ok(assignment);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("connect-worker-to-assignment")]
       public async Task<ActionResult<AssignmentWithWorkerResponseDTO>> ConnectWorkerToAssignment(AssignmentWorkerConnectionDTO assignmentWorkerConnectionDTO)
       {
            var assignment = await _assignmentService.ConnectWorkerToAssignment(assignmentWorkerConnectionDTO);
            if (assignment is null)
            {
                return NotFound("Assignment or worker not found ");
            }
            return Ok(assignment);
       }
        [Authorize]
        [HttpPost("start-assignment/{assignmentId:guid}")]
        public async  Task<ActionResult<AssignmentResponseDTO>> StartAssignment(Guid assignmentId)
        {
            var assignment = await _assignmentService.StartAssignment(assignmentId);
            if (assignment is null)
            {
                return BadRequest("Assignment status must be confirmed and be connected with user worker and building.");
            }
            return Ok(assignment);
        }
        [Authorize]
        [HttpPost("complete-assignment/{assignmentId:guid}")]
        public async Task<ActionResult<AssignmentResponseDTO>> CompleteAssignment(Guid assignmentId)
        {
            var assignment = await _assignmentService.CompleteAssignment(assignmentId);
            if (assignment is null)
            {
                return BadRequest("Assignment status must be started and be connected with user worker and building");
            }
            return Ok(assignment);
        }
        [Authorize]
        [HttpPost("cancel-assignment/{assignmentId:guid}")]
        public async Task<ActionResult<AssignmentResponseDTO>> CancelAssignment(Guid assignmentId)
        {
            var assignment = await _assignmentService.CancelAssignment(assignmentId);
            if (assignment is null)
            {
                return BadRequest("Assignment status must be started and be connected with user worker and building");
            }
            return Ok(assignment);
        }
        [Authorize]
        [HttpPost("reject-assignment/{assignmentId:guid}")]
        public async Task<ActionResult<AssignmentResponseDTO>> RejectAssignment(Guid assignmentId)
        {
            var assignment = await _assignmentService.RejectAssignment(assignmentId);
            if (assignment is null)
            {
                return BadRequest("Assignment status cannot be started or completed and be connected with user worker and building");
            }
            return Ok(assignment);
        }
        [Authorize]
        [HttpPost("confirm-assignment/{assignmentId:guid}")]
        public async Task<ActionResult<AssignmentResponseDTO>> ConfirmAssignment(Guid assignmentId)
        {
            var assignment = await _assignmentService.ConfirmAssignment(assignmentId);
            if (assignment is null)
            {
                return BadRequest("Assignment status must be requested and be connected with user worker and building");
            }
            return Ok(assignment);
        }
        [Authorize]
        [HttpGet("get-all-assignments")] 
        public async Task<ActionResult<AssignmentResponseDTO>> GetAllAssignments()
        {
            var allAssignments = await _assignmentService.GetAllAssignments();
            return Ok(new
            {
                Message = allAssignments.Count == 0 ? "No assignments are yet to be created" : null,
              Assignments = allAssignments
            });
        }
    }
}
