using ConstructionWebAPI.Data;
using ConstructionWebAPI.DTOS.AssignmentDTOS;
using ConstructionWebAPI.DTOS.BuildingDTOS;
using ConstructionWebAPI.DTOS.UserDTOS;
using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConstructionWebAPI.Services
{
    public class UserService : IUserService
    {
        public AppDbContext _dbContext;
        public UserService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<UserResponseDTO?> GetUserData(Guid guid)
        {
            var user = await _dbContext.Users
       .FirstOrDefaultAsync(u => u.Id == guid);

            if (user is null) return null;

            return MapToDto(user);
        }

        public async Task<List<BuildingWithOwnerResponseDTO>> SeeUserBuildings(Guid guid)
        {
            var buildings = await _dbContext.Buildings
           .AsNoTracking()
         .Include(b => b.Owner)
        .Where(b => b.OwnerId == guid)
        .Select(b => new BuildingWithOwnerResponseDTO
        {
            Id = b.Id,
            Name = b.Name,
            Address = b.Address,
            Price = b.Price,
            BuildingType = b.BuildingType,
            OwnerId = b.Owner.Id,
            OwnerEmail = b.Owner.Email
        })
        .ToListAsync();

    return buildings;
        }

        public async Task<UserResponseDTO?> UpdateUserData(Guid guid, UserRegisterDTO userRegisterDTO)
        {
            var user = await _dbContext.Users.FindAsync(guid);
            if (user is null) return null;

            user.FirstName = userRegisterDTO.FirstName;
            user.LastName = userRegisterDTO.LastName;
            user.Gender = userRegisterDTO.Gender;
            user.Email = userRegisterDTO.Email;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password);
            await _dbContext.SaveChangesAsync();
            return MapToDto(user); ;
        }
        private static UserResponseDTO MapToDto(User user)
        {
            return new UserResponseDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
        }

        public async Task<List<AssignmentsWithUserResponseDTO>> SeeUserAssignments(Guid guid)
        {
            var assignments = await _dbContext.Assignments
        .AsNoTracking()
        .Include(a=>a.User)
       .Where(a => a.UserId== guid)
       .Select(a => new AssignmentsWithUserResponseDTO
       {
           UserId = a.UserId,
           AssignmentId = a.Id,
           EmailAddress = a.User.Email,
           Status = a.Status,
           CreatedAt = a.CreatedAt,
           Description = a.Description,
           StartTime = a.StartTime,
           Priority = a.Priority,
           EndTime = a.EndTime,
       })
       .ToListAsync();
            return assignments;
        }

        public async Task<bool> DeleteAccount(Guid guid)
        {
            var user = await _dbContext.Users.FindAsync(guid);
            if (user == null) return false;
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
