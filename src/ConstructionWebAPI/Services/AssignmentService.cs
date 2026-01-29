using ConstructionWebAPI.Data;
using ConstructionWebAPI.DTOS.AssignmentDTOS;
using ConstructionWebAPI.DTOS.WorkerDTOS;
using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Enums;
using ConstructionWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace ConstructionWebAPI.Services
{
    public class AssignmentService : IAssignmentService
    {
        public AppDbContext _dbContext;
        public AssignmentService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<AssignmentResponseDTO> CancelAssignment(Guid assignmentId)
        {
            var assignment = await _dbContext.Assignments
                 .FirstOrDefaultAsync(e =>
                e.Id == assignmentId &&
               e.UserId != Guid.Empty &&
                e.WorkerId != Guid.Empty &&
                e.BuildingId != Guid.Empty);
            if (assignment is null || assignment.Status != AssignmentStatus.Started)
            {
                return null;
            }
            assignment.Status = AssignmentStatus.Cancelled;
            await _dbContext.SaveChangesAsync();
            return MapDto(assignment);
        }

        public async Task<AssignmentResponseDTO> CompleteAssignment(Guid assignmentId)
        {
            var assignment = await _dbContext.Assignments
                  .FirstOrDefaultAsync(e =>
                 e.Id == assignmentId &&
                e.UserId != Guid.Empty &&
                 e.WorkerId != Guid.Empty &&
                 e.BuildingId != Guid.Empty);
            if (assignment is null || assignment.Status!=AssignmentStatus.Started)
            {
                return null;
            }
            assignment.Status = AssignmentStatus.Completed;
            assignment.EndTime = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return MapDto(assignment);
        }

        public async Task<AssignmentsWithUserResponseDTO> ConnectUserToAssignment(Guid userId,AssignmentUserConnectionDTO assignmentUserConnectionDTO)
        {
            var normalisedEmail = assignmentUserConnectionDTO.EmailAddress.Trim().ToLower();
            var user = await _dbContext.Users.FirstOrDefaultAsync(e=>e.Id==userId && e.Email== normalisedEmail);
            if (user is null)
            {
                return null;
            }
            var assignment = await _dbContext.Assignments.Include(e=>e.User).FirstOrDefaultAsync(e=>e.Id==assignmentUserConnectionDTO.AssignmentId);
            if (assignment is null)
            {
                return null;    
            }
            assignment.User = user;
            await _dbContext.SaveChangesAsync();
            return MapWithUserDto(assignment);

        }

        public async Task<AssignmentResponseDTO> CreateAssignment(AssignmentDTO assignmentDTO)
        {
            var duplicateExists = await _dbContext.Assignments.AnyAsync(a =>
                a.Description == assignmentDTO.Description &&
                a.Priority == assignmentDTO.Priority &&
                a.Status == AssignmentStatus.Requested &&
                a.UserId == null &&
                a.WorkerId == null &&
                a.BuildingId == null
            );
            if (duplicateExists)
            {
                return null;
            }

            var assignment = new Assignment
            {
                Description = assignmentDTO.Description,
                Priority = assignmentDTO.Priority,
                Status = AssignmentStatus.Requested,
                CreatedAt = DateTime.UtcNow,
            };
            await _dbContext.AddAsync(assignment);
            await _dbContext.SaveChangesAsync();

            return MapDto(assignment);
        }

        public async Task<bool> DeleteAssignment(Guid guid)
        {
            var assignment = await _dbContext.Assignments.FindAsync(guid);
            if (assignment is null)
            {
                return false;
            }
             _dbContext.Remove(assignment);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<AssignmentResponseDTO> RejectAssignment(Guid assignmentId)
        {
            var assignment = await _dbContext.Assignments
               .FirstOrDefaultAsync(e =>
              e.Id == assignmentId &&
             e.UserId != null &&
              e.WorkerId != null &&
              e.BuildingId != null);
            if (assignment is null || assignment.Status == AssignmentStatus.Started || assignment.Status==AssignmentStatus.Completed)
            {
                return null;
            }
            assignment.Status = AssignmentStatus.Rejected;
            await _dbContext.SaveChangesAsync();
            return MapDto(assignment);
        }

        public async Task<AssignmentResponseDTO> StartAssignment(Guid assignmentId)
        {
            var assignment = await _dbContext.Assignments
               .FirstOrDefaultAsync(e =>
              e.Id == assignmentId &&
             e.UserId != null &&
              e.WorkerId != null &&
              e.BuildingId != null);
            if (assignment is null || assignment.Status != AssignmentStatus.Confirmed)
            {
                return null;
            }
            assignment.Status = AssignmentStatus.Started;
            assignment.StartTime = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return MapDto(assignment);
        }
        private static AssignmentResponseDTO MapDto(Assignment assignment)
        {
            return new AssignmentResponseDTO
            {
                AssignmentId = assignment.Id,
                CreatedAt = assignment.CreatedAt,
                Description=assignment.Description,
                Status=assignment.Status,
                Priority=assignment.Priority,
                StartTime=assignment.StartTime,
                EndTime=assignment.EndTime
            };
    
        }
        private static AssignmentsWithUserResponseDTO MapWithUserDto(Assignment assignment)
        {
            return new AssignmentsWithUserResponseDTO
            {
                UserId = assignment.UserId,
                AssignmentId=assignment.Id,
                EmailAddress = assignment.User.Email,
                Status=assignment.Status,
                CreatedAt=assignment.CreatedAt,
                Description=assignment.Description,
                StartTime=assignment.StartTime,
                Priority=assignment.Priority,
                EndTime=assignment.EndTime,
            };

        }
        private static AssignmentWithBuildingResponseDTO MapWithBuildingDTO(Assignment assignment)
        {
            return new AssignmentWithBuildingResponseDTO
            {
                BuildingId = assignment.BuildingId,
                AssignmentId = assignment.Id,
                BuildingName = assignment.Building.Name,
                Status = assignment.Status,
                CreatedAt = assignment.CreatedAt,
                Description = assignment.Description,
                StartTime = assignment.StartTime,
                Priority = assignment.Priority,
                EndTime = assignment.EndTime,
                BuildingAddress = assignment.Building.Address
            };

        }
        private static AssignmentWithWorkerResponseDTO MapWithWorkerDto(Assignment assignment)
        {
            return new AssignmentWithWorkerResponseDTO
            {
                WorkerId = assignment.WorkerId,
                FullName = $"{assignment.Worker.FirstName} {assignment.Worker.LastName}",
                Status = assignment.Status,
                AssignmentId=assignment.Id,
                CreatedAt=assignment.CreatedAt,
                StartTime=assignment.StartTime,
                EndTime=assignment.EndTime,
                Description=assignment.Description,
                Priority=assignment.Priority,
            };
        }

        public async Task<AssignmentResponseDTO?> ConfirmAssignment(Guid assignmentId)
        {
            var assignment = await _dbContext.Assignments
          .FirstOrDefaultAsync(e =>
              e.Id == assignmentId &&
              e.UserId != null &&
               e.WorkerId != null &&
               e.BuildingId != null);
            if (assignment is null || assignment.Status != AssignmentStatus.Requested)
            {
                return null;
            }
            assignment.Status = AssignmentStatus.Confirmed;
            await _dbContext.SaveChangesAsync();
            return MapDto(assignment);
        }

        public async Task<AssignmentWithBuildingResponseDTO?> ConnectBuildingToAssignment(AssignmentBuildingConnectionDTO assignmentWithBuilding)
        {
            var building = await _dbContext.Buildings
                   .FirstOrDefaultAsync(e => e.Id == assignmentWithBuilding.BuildingId && e.Name == assignmentWithBuilding.Name && e.Address == assignmentWithBuilding.Address);
            if (building is null)
            {
                return null;
            }
            var assignment = await _dbContext.Assignments.Include(e => e.Building).FirstOrDefaultAsync(e => e.Id == assignmentWithBuilding.AssignmentId);
            if (assignment is null)
            {
                return null;
            }
            assignment.Building = building;
            await _dbContext.SaveChangesAsync();
            return MapWithBuildingDTO(assignment);
        }

        public async Task<AssignmentWithWorkerResponseDTO?> ConnectWorkerToAssignment(AssignmentWorkerConnectionDTO assignmentWorkerConnectionDTO)
        {
            var worker = await _dbContext.Workers
                .FirstOrDefaultAsync(
                e=>e.Id==assignmentWorkerConnectionDTO.WorkerId &&
                e.FirstName==assignmentWorkerConnectionDTO.FirstName&&
                e.LastName==assignmentWorkerConnectionDTO.LastName);
            if (worker is null)
            {
                return null;
            }
            var assignment = await _dbContext.Assignments.Include(e=>e.Worker).FirstOrDefaultAsync(e=>e.Id==assignmentWorkerConnectionDTO.AssignmentId);
            if (assignment is null)
            {
                return null;
            }
            assignment.Worker = worker; 
            await _dbContext.SaveChangesAsync();
            return MapWithWorkerDto(assignment);

        }

        public async  Task<AssignmentResponseDTO?> GetAssignmentDetails(Guid guid)
        {
            var assignment = await _dbContext.Assignments.FindAsync(guid);
            if (assignment is null)
            {
                return null;
            }
            return MapDto(assignment);
        }

        public async Task<AssignmentResponseDTO?> UpdateAssignmentData(Guid guid, AssignmentDTO dto)
        {
            var assignment = await _dbContext.Assignments.FindAsync(guid);
            if (assignment is null)
            {
                return null;
            }
            assignment.Description = dto.Description;
            assignment.Priority = dto.Priority;
            await _dbContext.SaveChangesAsync();
            return MapDto(assignment);
        }

        public async Task<List<AssignmentResponseDTO>> GetAllAssignments()
        {
            return await _dbContext.Assignments
          .AsNoTracking() 
          .Select(assignment => new AssignmentResponseDTO
          {
              AssignmentId = assignment.Id,
              CreatedAt = assignment.CreatedAt,
              Description = assignment.Description,
              Status = assignment.Status,
              Priority = assignment.Priority,
              StartTime = assignment.StartTime,
              EndTime = assignment.EndTime
          })
          .ToListAsync();
        }
    }
}
