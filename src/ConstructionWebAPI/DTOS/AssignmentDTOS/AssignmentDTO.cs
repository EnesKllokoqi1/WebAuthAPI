using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConstructionWebAPI.DTOS.AssignmentDTOS
{
    public class AssignmentDTO
    {
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 500 characters.")]
        [Display(Name = "Task Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a priority level.")]
        [EnumDataType(typeof(PriorityEnum), ErrorMessage = "Invalid Priority selected.")]
        public PriorityEnum Priority { get; set; }
    }
}
