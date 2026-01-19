using ConstructionWebAPI.DTOS;
using ConstructionWebAPI.DTOS.UserDTOS;
using ConstructionWebAPI.Entities;
namespace ConstructionWebAPI.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserRegisterDTO userDTO);
        Task<TokenResponseDTO?> LogInAsync(UserLoginDTO userLoginDTO);
        Task<TokenResponseDTO?> RefreshToken(RefreshTokenRequestDTO request);

    }
}
