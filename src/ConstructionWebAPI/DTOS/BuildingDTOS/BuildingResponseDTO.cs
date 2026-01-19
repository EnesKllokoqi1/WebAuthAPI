using ConstructionWebAPI.Enums;

namespace ConstructionWebAPI.DTOS.BuildingDTOS
{
    public class BuildingResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public BuildingTypeEnum BuildingType { get; set; }
    }
}
