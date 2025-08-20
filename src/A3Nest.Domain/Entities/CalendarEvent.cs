using A3Nest.Domain.Common;
using A3Nest.Domain.ValueObjects;

namespace A3Nest.Domain.Entities;

public class CalendarEvent : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public bool IsAllDay { get; set; } = false;
    public string Location { get; set; } = string.Empty;
    public int OwnerId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public bool IsRecurring { get; set; } = false;
    public string? RecurrencePattern { get; set; }
    public List<int> AttendeeIds { get; set; } = new();
    public string? Notes { get; set; }
    public int? PropertyId { get; set; }
    public int? UnitId { get; set; }
    
    // Navigation properties
    public virtual User Owner { get; set; } = null!;
    public virtual Property? Property { get; set; }
    public virtual Unit? Unit { get; set; }
    
    public TimeSpan Duration => EndDateTime - StartDateTime;
}