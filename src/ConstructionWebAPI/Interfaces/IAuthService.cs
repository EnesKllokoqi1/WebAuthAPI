using ConstructionWebAPI.DTOS;
using ConstructionWebAPI.DTOS.UserDTOS;
using ConstructionWebAPI.Entities;
namespace ConstructionWebAPI.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseDTO?> RegisterAsync(UserRegisterDTO userDTO);
        Task<TokenResponseDTO?> LogInAsync(UserLoginDTO userLoginDTO);
        Task<bool> LogOutUser(Guid guid);
        Task<TokenResponseDTO?> RefreshToken(RefreshTokenRequestDTO request);

    }
}
