using A3Nest.Application.DTOs;

namespace A3Nest.Application.Commands.Tenants;

public class CreateTenantCommand
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string SocialSecurityNumber { get; set; } = string.Empty;
    public ContactInfoDto ContactInfo { get; set; } = new();
    public AddressDto CurrentAddress { get; set; } = new();
    public string EmploymentStatus { get; set; } = string.Empty;
    public MoneyDto MonthlyIncome { get; set; } = new();
    public string? EmergencyContactName { get; set; }
    public string? EmergencyContactPhone { get; set; }
}