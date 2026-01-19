using ConstructionWebAPI.DTOS.BuildingDTOS;

namespace ConstructionWebAPI.DTOS.UserDTOS
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<BuildingResponseDTO> Buildings { get; set; } = new();
    }
}
