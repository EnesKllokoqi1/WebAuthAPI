namespace ConstructionWebAPI.DTOS.AssignmentDTOS
{
    public class AssignmentWithBuildingResponseDTO : AssignmentResponseDTO
    {

        public Guid? BuildingId { get; set; }
        public string BuildingName { get; set; }
        public required string BuildingAddress { get; set; }
    }
}
