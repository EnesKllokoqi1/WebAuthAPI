using ConstructionWebAPI.DTOS.AssignmentDTOS;
using ConstructionWebAPI.DTOS.BuildingDTOS;
using ConstructionWebAPI.DTOS.UserDTOS;
using ConstructionWebAPI.Entities;

namespace ConstructionWebAPI.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO?> GetUserData(Guid guid);
        Task<UserResponseDTO?> UpdateUserData(Guid guid,UserRegisterDTO userRegisterDTO);
        Task<bool> DeleteAccount(Guid guid);
        Task<List<BuildingWithOwnerResponseDTO>> SeeUserBuildings(Guid guid);
        Task<List<AssignmentsWithUserResponseDTO>> SeeUserAssignments(Guid guid);
    }
}
