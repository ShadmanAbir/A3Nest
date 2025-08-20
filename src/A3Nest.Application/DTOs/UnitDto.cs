namespace A3Nest.Application.DTOs;

public class UnitDto
{
    public int Id { get; set; }
    public string UnitNumber { get; set; } = string.Empty;
    public int PropertyId { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public decimal SquareFootage { get; set; }
    public MoneyDto MonthlyRent { get; set; } = new();
    public MoneyDto SecurityDeposit { get; set; } = new();
    public bool IsAvailable { get; set; } = true;
    public string? Description { get; set; }
    public List<string> Amenities { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation DTOs
    public PropertyDto? Property { get; set; }
    public List<LeaseApplicationDto> LeaseApplications { get; set; } = new();
}