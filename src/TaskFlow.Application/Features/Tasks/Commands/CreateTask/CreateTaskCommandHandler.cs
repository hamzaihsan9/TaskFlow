using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Tasks.Commands.CreateTask;

/// <summary>
/// Handles the CreateTaskCommand by validating the input,
/// mapping it to a domain entity, and persisting it.
/// </summary>
public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskItemDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
    {
        _taskRepository = taskRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<TaskItemDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // Validate category exists if provided
        if (request.TaskDto.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(request.TaskDto.CategoryId.Value);
            if (category is null)
                throw new NotFoundException(nameof(Category), request.TaskDto.CategoryId.Value);
        }

        // Parse priority enum from string
        if (!Enum.TryParse<Priority>(request.TaskDto.Priority, true, out var priority))
            throw new BadRequestException($"Invalid priority value: {request.TaskDto.Priority}");

        // Map DTO to domain entity
        var taskItem = new TaskItem
        {
            Title = request.TaskDto.Title,
            Description = request.TaskDto.Description,
            Priority = priority,
            DueDate = request.TaskDto.DueDate,
            CategoryId = request.TaskDto.CategoryId,
            Status = TaskItemStatus.Pending
        };

        // Persist
        var created = await _taskRepository.AddAsync(taskItem);

        // Load the category for the response if it exists
        var result = await _taskRepository.GetTaskWithCategoryAsync(created.Id);

        // Map entity back to DTO
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
