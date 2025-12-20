using ConstructionWebAPI.Enums;


namespace ConstructionWebAPI.Entities
{
    public class Worker
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public GenderEnum Gender { get; set; }
        public decimal Salary { get; set; }
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
     
    }


}
