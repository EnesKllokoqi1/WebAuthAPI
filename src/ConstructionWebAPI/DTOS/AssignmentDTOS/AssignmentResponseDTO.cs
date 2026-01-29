using ConstructionWebAPI.Enums;

namespace ConstructionWebAPI.DTOS.AssignmentDTOS
{
    public class AssignmentResponseDTO : AssignmentDTO
    {
        public AssignmentStatus Status { get; set; }
        public Guid AssignmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
