namespace A3Nest.Application.DTOs;

public class CalendarEventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsAllDay { get; set; } = false;
    public string Location { get; set; } = string.Empty;
    public int OwnerId { get; set; }
    public List<int> AttendeeIds { get; set; } = new();
    public string? RecurrenceRule { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation DTOs
    public UserDto? Owner { get; set; }
    public List<UserDto> Attendees { get; set; } = new();
}