using ConstructionWebAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConstructionWebAPI.DTOS
{
    public class WorkerDTO
    {
     
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\-'\s]+$",
        ErrorMessage = "First name can only contain letters, hyphens, apostrophes and spaces")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\-'\s]+$",
            ErrorMessage = "Last name can only contain letters, hyphens, apostrophes and spaces")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required")]
        public GenderEnum Gender { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public decimal Salary { get; set; }
    }
}
