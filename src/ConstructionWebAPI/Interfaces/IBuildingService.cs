using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Enums;
using ConstructionWebAPI.DTOS.BuildingDTOS;
namespace ConstructionWebAPI.Interfaces
{
    public interface IBuildingService
    {
        Task<BuildingResponseDTO?> PurchaseBuilding(BuildingDTO builldingDTO);
        Task<IEnumerable<string>> GetAllBuildingTypes();
        Task<bool> DeleteBuilding(Guid guid);
        Task<BuildingResponseDTO?> UpdateBuildingData(Guid buildingId,BuildingDTO buildingDTO);
        Task<BuildingWithOwnerResponseDTO?> ConnectUserWithBuilding(Guid userId, UserBuildingConnectionDTO dto);
    }
}
