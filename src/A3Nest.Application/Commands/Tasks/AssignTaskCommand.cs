namespace A3Nest.Application.Commands.Tasks;

public class AssignTaskCommand
{
    public int TaskId { get; set; }
    public int AssignedToId { get; set; }
    public string? Notes { get; set; }
}