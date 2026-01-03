using ConstructionWebAPI.Interfaces;
using ConstructionWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConstructionWebAPI.Data
{
    public static class AdminUserSeeder
    {
       
        public static async Task RegisterAdmin(IServiceProvider serviceProvider)
        {
           
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var authService = scope.ServiceProvider.GetRequiredService <IAuthService>();
            var Admin = new User
            {
                FirstName = "Super",
                LastName = "Admin",
                Gender = Enums.GenderEnum.Male,
                Email="adminconstruction@gmail.com",
                UserRole=Enums.UserRoleEnum.Admin,
                PasswordHash=BCrypt.Net.BCrypt.HashPassword("secretAdmin1Password$")
            };
            var isAdmin = await dbContext.Users.AnyAsync(e => e.UserRole == Enums.UserRoleEnum.Admin);
            if (isAdmin)
            {
                return;
            }
            await dbContext.Users.AddAsync(Admin);
            await dbContext.SaveChangesAsync();
            return; 
        }
    }
}
