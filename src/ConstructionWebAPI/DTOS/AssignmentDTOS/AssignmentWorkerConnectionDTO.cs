using System.ComponentModel.DataAnnotations;

namespace ConstructionWebAPI.DTOS.AssignmentDTOS
{
    public class AssignmentWorkerConnectionDTO 
    {
        [Required(ErrorMessage = "Worker ID is required.")]
        public Guid WorkerId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [RegularExpression(
            @"^[\p{L}]+([\p{L}\s'\-]*[\p{L}])$",
            ErrorMessage = "First name may contain letters, spaces, hyphens, and apostrophes only."
        )]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [RegularExpression(
            @"^[\p{L}]+([\p{L}\s'\-]*[\p{L}])$",
            ErrorMessage = "Last name may contain letters, spaces, hyphens, and apostrophes only."
        )]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Assignment ID is required.")]
        public Guid AssignmentId { get; set; }
    }
}
