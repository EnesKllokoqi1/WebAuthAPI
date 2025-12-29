using ConstructionWebAPI.Data;
using ConstructionWebAPI.DTOS;
using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace ConstructionWebAPI.Services
{
    public class AuthService : IAuthService
    {
        public AppDbContext _dbContext;
        public IConfiguration _configuration;
        public AuthService(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        
  

        public async Task<TokenResponseDTO?> RefreshToken(RefreshTokenRequestDTO request)
        {
            var userWithRT = await _dbContext.Users.FirstOrDefaultAsync(e => e.RefreshToken == request.RefreshToken);
            if (userWithRT is null) { 
                return null; 
            }
            if (userWithRT.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                userWithRT.RefreshToken = null;
                await _dbContext.SaveChangesAsync();
                return null;
            }
            return await CreateTokens(userWithRT);
         
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.FirstName),
                 new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new(ClaimTypes.Email,user.Email),
                 new(ClaimTypes.Gender,user.Gender.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddMinutes(45)
                );
           return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public async Task<User?> RegisterAsync(UserRegisterDTO userDTO)
        {
            var normalizedEmail = userDTO.Email.Trim().ToLower();
            var existingUser = await _dbContext.Users
           .FirstOrDefaultAsync(u => u.Email == normalizedEmail);
            if (existingUser is not null)
            {
                return null;
            }
            var user = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                Email = userDTO.Email,
                Gender = userDTO.Gender,
                RefreshToken = "",
                RefreshTokenExpiryTime = DateTime.MinValue 
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<TokenResponseDTO> CreateTokens(User user)
            {
            var tokenResponseDto = new TokenResponseDTO
            {
                AcessToken =  CreateToken(user),
                RefreshToken =await GenerateRefreshToken(user)
            };
            return tokenResponseDto;
            }

        private async Task<string> GenerateRefreshToken(User user)
        {
           var refreshToken= Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime= DateTime.UtcNow.AddDays(7);
            await _dbContext.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<TokenResponseDTO?> LogInAsync(UserLoginDTO userLoginDTO)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == userLoginDTO.EmailAddress);
            if(user is null) { return null; }
            if (!BCrypt.Net.BCrypt.Verify(userLoginDTO.Password, user.PasswordHash))
            {
                return null;
            }
            return await CreateTokens(user);
        }
    }
}

