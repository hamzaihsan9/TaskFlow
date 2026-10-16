using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Tasks.Commands.UpdateTask;

/// <summary>
/// Command to update an existing task item.
/// </summary>
public class UpdateTaskCommand : IRequest<TaskItemDto>
{
    public UpdateTaskDto TaskDto { get; set; } = null!;
}
