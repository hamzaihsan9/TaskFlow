using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Tasks.Commands.CreateTask;

/// <summary>
/// Command to create a new task item.
/// Implements the Command side of CQRS — this changes state.
/// </summary>
public class CreateTaskCommand : IRequest<TaskItemDto>
{
    public CreateTaskDto TaskDto { get; set; } = null!;
}
