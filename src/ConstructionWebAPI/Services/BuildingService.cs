using ConstructionWebAPI.Data;
using ConstructionWebAPI.DTOS.BuildingDTOS;
using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Enums;
using ConstructionWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConstructionWebAPI.Services
{
    public class BuildingService : IBuildingService
    {
        public AppDbContext _dbContext;
        public BuildingService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BuildingWithOwnerResponseDTO?> ConnectUserWithBuilding(Guid userId, UserBuildingConnectionDTO dto)
        {
            var normalisedEmail = dto.EmailAddress.Trim().ToLower();
            var user = await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Id == userId && u.Email == normalisedEmail);

            if (user is null)
                return null;

            var building = await _dbContext.Buildings.FindAsync(dto.BuildingId);
            if (building is null)
                return null;

            building.OwnerId = user.Id;
            await _dbContext.SaveChangesAsync();

            return new BuildingWithOwnerResponseDTO
            {
                Id = building.Id,
                Name = building.Name,
                Address = building.Address,
                Price = building.Price,
                BuildingType = building.BuildingType,
                OwnerId = user.Id,
                OwnerEmail = user.Email
            };
        }

        public async Task<BuildingResponseDTO?> PurchaseBuilding(BuildingDTO buildingDTO)
        {

            var exists = await _dbContext.Buildings.AnyAsync(e =>
                e.Name == buildingDTO.Name &&
                e.Address == buildingDTO.Address);

            if (exists)
                return null;

            var building = new Building
            {
                Name = buildingDTO.Name,
                Address = buildingDTO.Address,
                Price = buildingDTO.Price,
                BuildingType = buildingDTO.BuildingType
            };

            await _dbContext.Buildings.AddAsync(building);
            await _dbContext.SaveChangesAsync();

            return MapToDto(building);
        }

        public async Task<bool> DeleteBuilding(Guid guid)
        {
            var building = await _dbContext.Buildings.FindAsync(guid);
            if (building is null)
            {
                return false;
            }
            _dbContext.Remove(building);
            await _dbContext.SaveChangesAsync();
            return true;
        }
            

        public  Task<IEnumerable<string>> GetAllBuildingTypes()
        {
            var types = Enum.GetNames<BuildingTypeEnum>().ToList();
            return Task.FromResult<IEnumerable<string>>(types);
        }

        public async Task<BuildingResponseDTO?> UpdateBuildingData(Guid buildingId, BuildingDTO buildingDTO)
        {
            var building = await _dbContext.Buildings.FindAsync(buildingId);
            if (building is null)
                return null;

            if (building.OwnerId != null)
                return null;

            building.Name = buildingDTO.Name;
            building.Address = buildingDTO.Address;
            building.Price = buildingDTO.Price;
            building.BuildingType = buildingDTO.BuildingType;

            await _dbContext.SaveChangesAsync();

            return MapToDto(building);
        }
        private static BuildingResponseDTO MapToDto(Building building)
        {
            return new BuildingResponseDTO
            {
                Id = building.Id,
                Name = building.Name,
                Address = building.Address,
                Price = building.Price,
                BuildingType = building.BuildingType
            };
        }



        }
}
