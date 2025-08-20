using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Tasks;
using A3Nest.Application.Queries.Tasks;

namespace A3Nest.Application.Interfaces;

public interface ITaskService
{
    Task<TaskDto> GetTaskAsync(int id);
    Task<IEnumerable<TaskDto>> GetTasksAsync();
    Task<IEnumerable<TaskDto>> GetTasksByAssigneeAsync(int assigneeId);
    Task<IEnumerable<TaskDto>> GetTasksByCreatorAsync(int creatorId);
    Task<IEnumerable<TaskDto>> GetOverdueTasksAsync();
    Task<TaskDto> CreateTaskAsync(CreateTaskCommand command);
    Task<TaskDto> UpdateTaskAsync(UpdateTaskCommand command);
    Task<TaskDto> AssignTaskAsync(AssignTaskCommand command);
    Task<TaskDto> CompleteTaskAsync(int taskId);
    Task DeleteTaskAsync(int id);
    Task<IEnumerable<TaskDto>> SearchTasksAsync(SearchTasksQuery query);
}