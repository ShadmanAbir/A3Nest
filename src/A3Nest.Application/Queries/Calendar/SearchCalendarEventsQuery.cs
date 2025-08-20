namespace A3Nest.Application.Queries.Calendar;

public class SearchCalendarEventsQuery
{
    public int UserId { get; set; }
    public string? SearchTerm { get; set; }
    public string? Title { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Location { get; set; }
    public bool? IsAllDay { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}