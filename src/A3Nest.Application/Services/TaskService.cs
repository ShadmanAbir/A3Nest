using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Tasks;
using A3Nest.Application.Queries.Tasks;
using A3Nest.Application.Interfaces;

namespace A3Nest.Application.Services;

public class TaskService : ITaskService
{
    public Task<TaskDto> GetTaskAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskDto>> GetTasksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskDto>> GetTasksByAssigneeAsync(int assigneeId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskDto>> GetTasksByCreatorAsync(int creatorId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskDto>> GetOverdueTasksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TaskDto> CreateTaskAsync(CreateTaskCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<TaskDto> UpdateTaskAsync(UpdateTaskCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<TaskDto> AssignTaskAsync(AssignTaskCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<TaskDto> CompleteTaskAsync(int taskId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTaskAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskDto>> SearchTasksAsync(SearchTasksQuery query)
    {
        throw new NotImplementedException();
    }
}