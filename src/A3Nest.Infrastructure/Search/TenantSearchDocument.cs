using Nest;

namespace A3Nest.Infrastructure.Search;

[ElasticsearchType(RelationName = "tenant")]
public class TenantSearchDocument
{
    [Number(Name = "id")]
    public int Id { get; set; }

    [Text(Name = "first_name", Analyzer = "standard")]
    public string FirstName { get; set; } = string.Empty;

    [Text(Name = "last_name", Analyzer = "standard")]
    public string LastName { get; set; } = string.Empty;

    [Text(Name = "full_name", Analyzer = "standard")]
    public string FullName { get; set; } = string.Empty;

    [Date(Name = "date_of_birth")]
    public DateTime DateOfBirth { get; set; }

    [Keyword(Name = "social_security_number")]
    public string SocialSecurityNumber { get; set; } = string.Empty;

    [Object(Name = "contact_info")]
    public ContactInfoSearchDocument ContactInfo { get; set; } = new();

    [Object(Name = "current_address")]
    public AddressSearchDocument CurrentAddress { get; set; } = new();

    [Text(Name = "employment_status", Analyzer = "standard")]
    public string EmploymentStatus { get; set; } = string.Empty;

    [Number(Name = "monthly_income")]
    public decimal MonthlyIncome { get; set; }

    [Text(Name = "emergency_contact_name", Analyzer = "standard")]
    public string? EmergencyContactName { get; set; }

    [Keyword(Name = "emergency_contact_phone")]
    public string? EmergencyContactPhone { get; set; }

    [Boolean(Name = "is_active")]
    public bool IsActive { get; set; } = true;

    [Date(Name = "created_at")]
    public DateTime CreatedAt { get; set; }

    [Date(Name = "updated_at")]
    public DateTime UpdatedAt { get; set; }
}

public class ContactInfoSearchDocument
{
    [Keyword(Name = "email")]
    public string Email { get; set; } = string.Empty;

    [Keyword(Name = "phone")]
    public string Phone { get; set; } = string.Empty;

    [Keyword(Name = "mobile")]
    public string Mobile { get; set; } = string.Empty;
}