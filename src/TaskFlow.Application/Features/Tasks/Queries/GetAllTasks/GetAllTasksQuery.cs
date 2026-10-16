using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Tasks.Queries.GetAllTasks;

/// <summary>
/// Query to retrieve all task items.
/// Implements the Query side of CQRS — this only reads state.
/// </summary>
public class GetAllTasksQuery : IRequest<IReadOnlyList<TaskItemDto>>
{
}
