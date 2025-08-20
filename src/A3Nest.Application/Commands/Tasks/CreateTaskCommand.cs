using A3Nest.Domain.Enums;

namespace A3Nest.Application.Commands.Tasks;

public class CreateTaskCommand
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? AssignedToId { get; set; }
    public int CreatedById { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; } = 1;
    public string? Notes { get; set; }
}