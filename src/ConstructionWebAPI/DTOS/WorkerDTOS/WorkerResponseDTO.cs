using ConstructionWebAPI.Enums;

namespace ConstructionWebAPI.DTOS.WorkerDTOS
{
    public class WorkerResponseDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public GenderEnum Gender { get; set; }
        public decimal Salary { get; set; }
    }
}
