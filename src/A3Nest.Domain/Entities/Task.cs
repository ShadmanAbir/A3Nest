using A3Nest.Domain.Common;
using A3Nest.Domain.Enums;
using A3Nest.Domain.ValueObjects;
using TaskStatus = A3Nest.Domain.Enums.TaskStatus;

namespace A3Nest.Domain.Entities;

public class Task : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; } = TaskStatus.New;
    public int AssignedToId { get; set; }
    public int? AssignedById { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string Priority { get; set; } = "Medium";
    public string? Category { get; set; }
    public int? PropertyId { get; set; }
    public int? UnitId { get; set; }
    public List<string> Tags { get; set; } = new();
    public string? Notes { get; set; }
    
    // Navigation properties
    public virtual User AssignedTo { get; set; } = null!;
    public virtual User? AssignedBy { get; set; }
    public virtual Property? Property { get; set; }
    public virtual Unit? Unit { get; set; }
}