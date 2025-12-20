using ConstructionWebAPI.Enums;

namespace ConstructionWebAPI.Entities
{
    public class Building
    {
        public Guid Id { get; set;}
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    
        public string Name { get; set;}
        public decimal Price { get; set; }
        public string Address { get; set; }
        public BuildingTypeEnum BuildingType { get; set; } 
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    }
}
