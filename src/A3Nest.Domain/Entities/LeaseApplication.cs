using A3Nest.Domain.Common;
using A3Nest.Domain.Enums;
using A3Nest.Domain.ValueObjects;

namespace A3Nest.Domain.Entities;

public class LeaseApplication : BaseEntity
{
    public int TenantId { get; set; }
    public int PropertyId { get; set; }
    public int UnitId { get; set; }
    public LeaseStatus Status { get; set; } = LeaseStatus.Pending;
    public DateRange RequestedLeaseTerms { get; set; } = new();
    public Money ProposedRent { get; set; } = Money.Zero;
    public Money SecurityDepositAmount { get; set; } = Money.Zero;
    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
    public DateTime? ApprovalDate { get; set; }
    public DateTime? RejectionDate { get; set; }
    public string? RejectionReason { get; set; }
    public string? Notes { get; set; }
    public int? ApprovedById { get; set; }
    
    // Navigation properties
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual Property Property { get; set; } = null!;
    public virtual Unit Unit { get; set; } = null!;
    public virtual User? ApprovedBy { get; set; }
}