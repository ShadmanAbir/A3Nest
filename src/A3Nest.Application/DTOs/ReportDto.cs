namespace A3Nest.Application.DTOs;

public class ReportDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ReportType { get; set; } = string.Empty;
    public int GeneratedById { get; set; }
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public DateRangeDto ReportPeriod { get; set; } = new();
    public Dictionary<string, object> Parameters { get; set; } = new();
    public Dictionary<string, object> Data { get; set; } = new();
    public string? FilePath { get; set; }
    public string Format { get; set; } = "PDF";
    public bool IsScheduled { get; set; } = false;
    public string? SchedulePattern { get; set; }
    public List<int> RecipientIds { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation DTOs
    public UserDto? GeneratedBy { get; set; }
}