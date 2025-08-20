using A3Nest.Domain.Enums;

namespace A3Nest.Application.Queries.Tasks;

public class SearchTasksQuery
{
    public string? SearchTerm { get; set; }
    public string? Title { get; set; }
    public A3Nest.Domain.Enums.TaskStatus? Status { get; set; }
    public int? AssignedToId { get; set; }
    public int? CreatedById { get; set; }
    public DateTime? DueDateFrom { get; set; }
    public DateTime? DueDateTo { get; set; }
    public int? Priority { get; set; }
    public bool? IsOverdue { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}