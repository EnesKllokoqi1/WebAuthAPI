using System.ComponentModel.DataAnnotations;

namespace ConstructionWebAPI.DTOS.AssignmentDTOS
{
    public class AssignmentBuildingConnectionDTO
    {
        [Required(ErrorMessage = "Building ID is required.")]
        public Guid BuildingId { get; set; }

        [Required(ErrorMessage = "Building name is required.")]
        [StringLength(100, ErrorMessage = "Building name cannot exceed 100 characters.")]
        [RegularExpression(
        @"^[\p{L}0-9\s\-]+$",
        ErrorMessage = "Building name can only contain letters, numbers, spaces, and dashes."
         )]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(150, ErrorMessage = "Address cannot exceed 150 characters.")]
        [RegularExpression(
         @"^[\p{L}0-9\s,.\-\/#]+$",
         ErrorMessage = "Address contains invalid characters."
         )]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Assignment ID is required.")]
        public Guid AssignmentId { get; set; }
    }
}
