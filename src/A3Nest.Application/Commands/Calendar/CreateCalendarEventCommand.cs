namespace A3Nest.Application.Commands.Calendar;

public class CreateCalendarEventCommand
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsAllDay { get; set; } = false;
    public string Location { get; set; } = string.Empty;
    public int OwnerId { get; set; }
    public List<int> AttendeeIds { get; set; } = new();
    public string? RecurrenceRule { get; set; }
}