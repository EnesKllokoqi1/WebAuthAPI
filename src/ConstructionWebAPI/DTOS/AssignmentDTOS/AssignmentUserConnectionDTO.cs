using System.ComponentModel.DataAnnotations;

namespace ConstructionWebAPI.DTOS.AssignmentDTOS
{
    public class AssignmentUserConnectionDTO
    {
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Assignment ID is required.")]
        public Guid AssignmentId { get; set; }

    }
}
