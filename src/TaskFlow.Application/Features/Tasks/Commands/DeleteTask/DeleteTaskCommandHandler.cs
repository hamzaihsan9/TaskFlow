using MediatR;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Tasks.Commands.DeleteTask;

/// <summary>
/// Handles deleting a task item.
/// </summary>
public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Unit>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var taskItem = await _taskRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(TaskItem), request.Id);

        await _taskRepository.DeleteAsync(taskItem);
        return Unit.Value;
    }
}
