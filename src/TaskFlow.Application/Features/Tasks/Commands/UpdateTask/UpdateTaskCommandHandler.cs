using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Tasks.Commands.UpdateTask;

/// <summary>
/// Handles updating an existing task item with new values.
/// </summary>
public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskItemDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
    {
        _taskRepository = taskRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<TaskItemDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        // Ensure the task exists
        var taskItem = await _taskRepository.GetByIdAsync(request.TaskDto.Id)
            ?? throw new NotFoundException(nameof(TaskItem), request.TaskDto.Id);

        // Validate category if provided
        if (request.TaskDto.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(request.TaskDto.CategoryId.Value)
                ?? throw new NotFoundException(nameof(Category), request.TaskDto.CategoryId.Value);
        }

        // Parse enums
        if (!Enum.TryParse<Priority>(request.TaskDto.Priority, true, out var priority))
            throw new BadRequestException($"Invalid priority: {request.TaskDto.Priority}");

        if (!Enum.TryParse<TaskItemStatus>(request.TaskDto.Status, true, out var status))
            throw new BadRequestException($"Invalid status: {request.TaskDto.Status}");

        // Update entity
        taskItem.Title = request.TaskDto.Title;
        taskItem.Description = request.TaskDto.Description;
        taskItem.Status = status;
        taskItem.Priority = priority;
        taskItem.DueDate = request.TaskDto.DueDate;
        taskItem.CategoryId = request.TaskDto.CategoryId;
        taskItem.UpdatedAt = DateTime.UtcNow;

        await _taskRepository.UpdateAsync(taskItem);

        // Reload with category
        var result = await _taskRepository.GetTaskWithCategoryAsync(taskItem.Id);

        return new TaskItemDto
        {
            Id = result!.Id,
            Title = result.Title,
            Description = result.Description,
            Status = result.Status.ToString(),
            Priority = result.Priority.ToString(),
            DueDate = result.DueDate,
            CategoryId = result.CategoryId,
            CategoryName = result.Category?.Name,
            CreatedAt = result.CreatedAt,
            UpdatedAt = result.UpdatedAt
        };
    }
}
