using A3Nest.Application.DTOs;
using A3Nest.Domain.Enums;

namespace A3Nest.Application.Commands.Properties;

public class CreatePropertyCommand
{
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
}