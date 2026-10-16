using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Tasks.Queries.GetTaskDetail;

/// <summary>
/// Handles retrieving a single task with its related category information.
/// </summary>
public class GetTaskDetailQueryHandler : IRequestHandler<GetTaskDetailQuery, TaskItemDto>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskDetailQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskItemDto> Handle(GetTaskDetailQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskWithCategoryAsync(request.Id)
            ?? throw new NotFoundException(nameof(TaskItem), request.Id);

        return new TaskItemDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status.ToString(),
            Priority = task.Priority.ToString(),
            DueDate = task.DueDate,
            CategoryId = task.CategoryId,
            CategoryName = task.Category?.Name,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };
    }
}
