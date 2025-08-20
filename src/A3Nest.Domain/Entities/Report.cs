using A3Nest.Domain.Common;
using A3Nest.Domain.ValueObjects;

namespace A3Nest.Domain.Entities;

public class Report : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ReportType { get; set; } = string.Empty;
    public int GeneratedById { get; set; }
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public DateRange ReportPeriod { get; set; } = new();
    public Dictionary<string, object> Parameters { get; set; } = new();
    public Dictionary<string, object> Data { get; set; } = new();
    public string? FilePath { get; set; }
    public string Format { get; set; } = "PDF";
    public bool IsScheduled { get; set; } = false;
    public string? SchedulePattern { get; set; }
    public List<int> RecipientIds { get; set; } = new();
    
    // Navigation properties
    public virtual User GeneratedBy { get; set; } = null!;
}