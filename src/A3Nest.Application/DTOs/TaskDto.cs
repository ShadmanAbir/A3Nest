using A3Nest.Domain.Enums;

namespace A3Nest.Application.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public A3Nest.Domain.Enums.TaskStatus Status { get; set; } = A3Nest.Domain.Enums.TaskStatus.New;
    public int? AssignedToId { get; set; }
    public int CreatedById { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int Priority { get; set; } = 1;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation DTOs
    public UserDto? AssignedTo { get; set; }
    public UserDto? CreatedBy { get; set; }
}