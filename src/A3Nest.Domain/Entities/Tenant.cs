using A3Nest.Domain.Common;
using A3Nest.Domain.ValueObjects;

namespace A3Nest.Domain.Entities;

public class Tenant : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string SocialSecurityNumber { get; set; } = string.Empty;
    public ContactInfo ContactInfo { get; set; } = ContactInfo.Empty;
    public Address CurrentAddress { get; set; } = Address.Empty;
    public string EmploymentStatus { get; set; } = string.Empty;
    public Money MonthlyIncome { get; set; } = Money.Zero;
    public string? EmergencyContactName { get; set; }
    public string? EmergencyContactPhone { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public virtual ICollection<LeaseApplication> LeaseApplications { get; set; } = new List<LeaseApplication>();
    
    public string FullName => $"{FirstName} {LastName}";
}