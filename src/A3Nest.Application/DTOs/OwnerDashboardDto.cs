namespace A3Nest.Application.DTOs;

public class OwnerDashboardDto
{
    public int TotalProperties { get; set; }
    public int TotalUnits { get; set; }
    public int OccupiedUnits { get; set; }
    public int VacantUnits { get; set; }
    public MoneyDto TotalMonthlyRent { get; set; } = new();
    public MoneyDto CollectedRent { get; set; } = new();
    public MoneyDto OutstandingRent { get; set; } = new();
    public int PendingApplications { get; set; }
    public int ActiveTasks { get; set; }
    public int UnreadMessages { get; set; }
    public List<PropertyDto> RecentProperties { get; set; } = new();
    public List<TaskDto> UpcomingTasks { get; set; } = new();
    public List<CalendarEventDto> UpcomingEvents { get; set; } = new();
}