using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Tasks.Queries.GetAllTasks;

/// <summary>
/// Handles retrieving all tasks from the repository.
/// </summary>
public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IReadOnlyList<TaskItemDto>>
{
    private readonly ITaskRepository _taskRepository;

    public GetAllTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IReadOnlyList<TaskItemDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync();

        return tasks.Select(t => new TaskItemDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status.ToString(),
            Priority = t.Priority.ToString(),
            DueDate = t.DueDate,
            CategoryId = t.CategoryId,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        }).ToList().AsReadOnly();
    }
}
