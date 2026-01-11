using ConstructionWebAPI.Data;
using ConstructionWebAPI.DTOS;
using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConstructionWebAPI.Services
{
    public class UserService : IUserService
    {
        public AppDbContext _dbContext;
        public UserService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<User?> GetUserData(Guid guid)
        {
            var user = await _dbContext.Users.FindAsync(guid);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<List<Building>> SeeUsersBuildings(Guid guid)
        {
            var userWithBuildings1 = await _dbContext.Buildings.Where(e => e.OwnerId == guid).ToListAsync();
            return userWithBuildings1;
        }

        public async Task<User?> UpdateUserData(Guid guid, UserRegisterDTO userRegisterDTO)
        {
            var theUser = await _dbContext.Users.FindAsync(guid);
            if (theUser is null)
            {
                return null;
            }
            theUser.FirstName = userRegisterDTO.FirstName;
            theUser.LastName = userRegisterDTO.LastName;
            theUser.Gender = userRegisterDTO.Gender;
            theUser.Email = userRegisterDTO.Email;
            theUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password);
            await _dbContext.SaveChangesAsync();
            return theUser;


        }
    }
}
