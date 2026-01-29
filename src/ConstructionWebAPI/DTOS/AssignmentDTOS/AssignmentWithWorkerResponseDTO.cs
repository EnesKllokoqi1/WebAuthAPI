namespace ConstructionWebAPI.DTOS.AssignmentDTOS
{
    public class AssignmentWithWorkerResponseDTO : AssignmentResponseDTO
    {
        public Guid? WorkerId { get; set; }
        public string FullName { get; set; }
    }
}
