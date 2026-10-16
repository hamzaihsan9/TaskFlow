using MediatR;

namespace TaskFlow.Application.Features.Tasks.Commands.DeleteTask;

/// <summary>
/// Command to delete a task item by its ID.
/// Returns Unit (void) since deletion doesn't need to return data.
/// </summary>
public class DeleteTaskCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
