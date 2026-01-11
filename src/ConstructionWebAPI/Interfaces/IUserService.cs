using ConstructionWebAPI.DTOS;
using ConstructionWebAPI.Entities;

namespace ConstructionWebAPI.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserData(Guid guid);
        Task<User?> UpdateUserData(Guid guid,UserRegisterDTO userRegisterDTO);
        Task<List<Building>> SeeUsersBuildings(Guid guid);
    }
}
