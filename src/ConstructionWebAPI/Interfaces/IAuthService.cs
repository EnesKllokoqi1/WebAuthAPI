using ConstructionWebAPI.DTOS;
using ConstructionWebAPI.Entities;
namespace ConstructionWebAPI.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync();
        Task<TokenResponseDTO?> LogInAsync();
        Task<TokenResponseDTO?> RefreshTokenDto(RefreshTokenRequestDTO request);

    }
}
