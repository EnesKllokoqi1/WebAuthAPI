using ConstructionWebAPI.Entities;
using ConstructionWebAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConstructionWebAPI.DTOS.BuildingDTOS
{
    public class BuildingDTO
    {
        [Required(ErrorMessage = "Building name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Building name must be between 3 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, 1_000_000_000, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Building type is required.")]
        [EnumDataType(typeof(BuildingTypeEnum), ErrorMessage = "Invalid building type.")]
        public BuildingTypeEnum BuildingType { get; set; }

    }
}
