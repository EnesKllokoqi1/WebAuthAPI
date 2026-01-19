using ConstructionWebAPI.DTOS.BuildingDTOS;
using ConstructionWebAPI.DTOS.UserDTOS;
using ConstructionWebAPI.Entities;

namespace ConstructionWebAPI.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO?> GetUserData(Guid guid);
        Task<UserResponseDTO?> UpdateUserData(Guid guid,UserRegisterDTO userRegisterDTO);
        Task<List<BuildingResponseDTO>> SeeUsersBuildings(Guid guid);
    }
}
