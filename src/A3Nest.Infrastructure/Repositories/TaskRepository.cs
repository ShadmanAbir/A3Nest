using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Tasks;
using A3Nest.Application.Queries.Tasks;

namespace A3Nest.Infrastructure.Repositories;

public class TaskRepository : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;

    public TaskRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TaskDto> GetTaskAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TaskDto>> GetTasksAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TaskDto>> GetTasksByAssigneeAsync(int assigneeId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TaskDto>> GetTasksByCreatorAsync(int creatorId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TaskDto>> GetOverdueTasksAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskDto> UpdateTaskAsync(UpdateTaskCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskDto> AssignTaskAsync(AssignTaskCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskDto> CompleteTaskAsync(int taskId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteTaskAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TaskDto>> SearchTasksAsync(SearchTasksQuery query)
    {
        throw new NotImplementedException();
    }
}