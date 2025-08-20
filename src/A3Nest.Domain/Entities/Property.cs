using A3Nest.Domain.Common;
using A3Nest.Domain.Enums;
using A3Nest.Domain.ValueObjects;

namespace A3Nest.Domain.Entities;

public class Property : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PropertyType PropertyType { get; set; }
    public Address Address { get; set; } = Address.Empty;
    public int TotalUnits { get; set; }
    public Money PurchasePrice { get; set; } = Money.Zero;
    public Money CurrentValue { get; set; } = Money.Zero;
    public DateTime PurchaseDate { get; set; }
    public int OwnerId { get; set; }
    public int? PropertyManagerId { get; set; }
    
    // Navigation properties
    public virtual User Owner { get; set; } = null!;
    public virtual User? PropertyManager { get; set; }
    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
    public virtual ICollection<LeaseApplication> LeaseApplications { get; set; } = new List<LeaseApplication>();
}