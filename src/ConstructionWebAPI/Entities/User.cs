using ConstructionWebAPI.Enums;
using System.ComponentModel.DataAnnotations;
namespace ConstructionWebAPI.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public GenderEnum Gender { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public ICollection<Building> Buildings { get; set; } = new List<Building>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
   

      

    }
}
