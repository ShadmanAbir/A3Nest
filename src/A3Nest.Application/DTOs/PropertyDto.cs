using A3Nest.Domain.Enums;

namespace A3Nest.Application.DTOs;

public class PropertyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PropertyType PropertyType { get; set; }
    public AddressDto Address { get; set; } = new();
    public int TotalUnits { get; set; }
    public MoneyDto PurchasePrice { get; set; } = new();
    public MoneyDto CurrentValue { get; set; } = new();
    public DateTime PurchaseDate { get; set; }
    public int OwnerId { get; set; }
    public int? PropertyManagerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation DTOs
    public UserDto? Owner { get; set; }
    public UserDto? PropertyManager { get; set; }
    public List<UnitDto> Units { get; set; } = new();
    public List<LeaseApplicationDto> LeaseApplications { get; set; } = new();
}