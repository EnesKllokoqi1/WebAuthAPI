namespace ConstructionWebAPI.DTOS.AssignmentDTOS
{
    public class AssignmentsWithUserResponseDTO : AssignmentResponseDTO
    {
        public Guid? UserId { get; set; }
        public required string EmailAddress { get; set; }
    }
}
