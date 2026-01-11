
using ConstructionWebAPI.DTOS;
using ConstructionWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public IAuthService _authService;
        public IAdminService _adminService;

        public AdminController(IAuthService authService, IAdminService adminService)
        {
            _authService = authService;
            _adminService = adminService;
        }

        [HttpPost("login-as-admin")]
        public async Task<ActionResult> LogInAsAdmin(UserLoginDTO userLoginDTO)
        {
            var admin = await _authService.LogInAsync(userLoginDTO);
            if (admin == null)
            {
                return Unauthorized(new { error = "Invalid email or password" });
            }

            return Ok(admin);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-all-workers")]
        public async Task<ActionResult> GetAllWorkers()
        {
            var workers = await _adminService.GetAllWorkers();
            return Ok(new
            {
                message = workers.Count == 0 ? "No workers are yet to be created" : null,
                data = workers
            });

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-worker-data-by-id/{id:guid}")]
        public async Task<ActionResult> GetWorkerDataById(Guid id)
        {
            var worker = await _adminService.GetWorkerDataById(id);
            if (worker is null)
            {
                return NotFound($"Worker with ID : {id} not found");
            }
            return Ok(worker);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-workers")]
        public async Task<ActionResult> CreateWorkers(WorkerDTO workerDTO)
        {
            var worker = await _adminService.CreateWorker(workerDTO);
            if (worker is null)
            {
                return BadRequest("Worker already exists");
            }
            return Ok(new   
            {
                Message = "Worker is created",
                Worker = worker
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-worker-data/{id:guid}")]
        public async Task<ActionResult> UpdateWorkersData(Guid id, WorkerDTO createWorkerDTO)
        {
            var worker = await _adminService.UpdateWorkerData(id, createWorkerDTO);
            if (worker is null)
            {
                return NotFound($"Worker with ID :{id} has not been found");
            }
            return Ok(worker);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(RefreshTokenRequestDTO refreshTokenRequestDTO)
        {
            var tokens = await _authService.RefreshToken(refreshTokenRequestDTO);
            if (tokens is null)
            {
                return BadRequest("Invalid refresh token");
            }
            return Ok(tokens);

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-worker-id/{id:guid}")]
        public async Task<ActionResult> DeleteWorker(Guid id)
        {
            var worker = await _adminService.DeleteWorker(id);
            if (!worker)
            {
                return NotFound($"Worker with id : {id} not found");
            }
            return NoContent();
        }



    }
}