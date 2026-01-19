namespace ConstructionWebAPI.DTOS.BuildingDTOS
{
    public class BuildingWithOwnerResponseDTO : BuildingResponseDTO
    {
        public Guid OwnerId { get; set; }
        public string? OwnerEmail { get; set; }
    }
}
