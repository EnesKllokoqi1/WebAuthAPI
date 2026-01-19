using ConstructionWebAPI.Data;
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
       .Include(u => u.Buildings)
       .FirstOrDefaultAsync(u => u.Id == guid);

            if (user is null) return null;

            return MapToDto(user);
        }

        public async Task<List<BuildingResponseDTO>> SeeUsersBuildings(Guid guid)
        {
            var buildings = await _dbContext.Buildings
        .Where(b => b.OwnerId == guid)
        .Select(b => new BuildingResponseDTO
        {
            Id = b.Id,
            Name = b.Name,
            Address = b.Address,
            Price = b.Price,
            BuildingType = b.BuildingType
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
                Buildings = user.Buildings.Select(b => new BuildingResponseDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                    Address = b.Address,
                    Price = b.Price,
                    BuildingType = b.BuildingType
                }).ToList()
            };
        }
    }
}
