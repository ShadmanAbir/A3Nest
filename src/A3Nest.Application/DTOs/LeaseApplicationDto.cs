using A3Nest.Domain.Enums;

namespace A3Nest.Application.DTOs;

public class LeaseApplicationDto
{
    public int Id { get; set; }
    public int TenantId { get; set; }
    public int PropertyId { get; set; }
    public int UnitId { get; set; }
    public LeaseStatus Status { get; set; } = LeaseStatus.Pending;
    public DateRangeDto RequestedLeaseTerms { get; set; } = new();
    public MoneyDto ProposedRent { get; set; } = new();
    public MoneyDto SecurityDepositAmount { get; set; } = new();
    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
    public DateTime? ApprovalDate { get; set; }
    public DateTime? RejectionDate { get; set; }
    public string? RejectionReason { get; set; }
    public string? Notes { get; set; }
    public int? ApprovedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation DTOs
    public TenantDto? Tenant { get; set; }
    public PropertyDto? Property { get; set; }
    public UnitDto? Unit { get; set; }
    public UserDto? ApprovedBy { get; set; }
}