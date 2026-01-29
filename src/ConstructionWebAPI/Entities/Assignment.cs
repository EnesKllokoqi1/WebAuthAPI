using ConstructionWebAPI.Enums;
namespace ConstructionWebAPI.Entities
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public Guid? WorkerId { get; set; }
        public Worker? Worker { get; set; }
        public Guid? BuildingId { get; set; }
        public Building? Building { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public AssignmentStatus Status { get; set; }
        public string Description { get; set; } =string.Empty;
        public PriorityEnum Priority { get; set; } 
    }
}
