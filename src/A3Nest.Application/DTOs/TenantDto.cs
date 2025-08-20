using A3Nest.Domain.Enums;

namespace A3Nest.Application.DTOs;

public class TenantDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public DateTime DateOfBirth { get; set; }
    public string SocialSecurityNumber { get; set; } = string.Empty;
    public ContactInfoDto ContactInfo { get; set; } = new();
    public AddressDto CurrentAddress { get; set; } = new();
    public string EmploymentStatus { get; set; } = string.Empty;
    public MoneyDto MonthlyIncome { get; set; } = new();
    public string EmergencyContactName { get; set; } = string.Empty;
    public string EmergencyContactPhone { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}