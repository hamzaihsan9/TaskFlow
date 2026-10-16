using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Tasks.Queries.GetTaskDetail;

/// <summary>
/// Query to retrieve a single task item with full details including category.
/// </summary>
public class GetTaskDetailQuery : IRequest<TaskItemDto>
{
    public int Id { get; set; }
}
