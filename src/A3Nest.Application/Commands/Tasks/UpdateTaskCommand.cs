using A3Nest.Domain.Enums;

namespace A3Nest.Application.Commands.Tasks;

public class UpdateTaskCommand
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public A3Nest.Domain.Enums.TaskStatus Status { get; set; }
    public int? AssignedToId { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; } = 1;
    public string? Notes { get; set; }
}