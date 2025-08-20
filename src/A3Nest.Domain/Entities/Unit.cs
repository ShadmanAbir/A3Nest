using A3Nest.Domain.Common;
using A3Nest.Domain.ValueObjects;

namespace A3Nest.Domain.Entities;

public class Unit : BaseEntity
{
    public string UnitNumber { get; set; } = string.Empty;
    public int PropertyId { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public decimal SquareFootage { get; set; }
    public Money MonthlyRent { get; set; } = Money.Zero;
    public Money SecurityDeposit { get; set; } = Money.Zero;
    public bool IsAvailable { get; set; } = true;
    public string? Description { get; set; }
    public List<string> Amenities { get; set; } = new();
    
    // Navigation properties
    public virtual Property Property { get; set; } = null!;
    public virtual ICollection<LeaseApplication> LeaseApplications { get; set; } = new List<LeaseApplication>();
}