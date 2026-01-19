using System.ComponentModel.DataAnnotations;

namespace ConstructionWebAPI.DTOS.BuildingDTOS
{
    public class UserBuildingConnectionDTO
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        [MaxLength(254, ErrorMessage = "Email address cannot exceed 254 characters")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Building Id is required")]
        public Guid BuildingId { get; set; }
    }
}
