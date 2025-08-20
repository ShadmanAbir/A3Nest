namespace A3Nest.Application.Commands.Calendar;

public class UpdateCalendarEventCommand
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsAllDay { get; set; } = false;
    public string Location { get; set; } = string.Empty;
    public List<int> AttendeeIds { get; set; } = new();
    public string? RecurrenceRule { get; set; }
}