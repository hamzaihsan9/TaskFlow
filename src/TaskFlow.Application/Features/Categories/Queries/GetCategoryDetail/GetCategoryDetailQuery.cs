using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Categories.Queries.GetCategoryDetail;

/// <summary>
/// Query to retrieve a single category with its tasks.
/// </summary>
public class GetCategoryDetailQuery : IRequest<CategoryDto>
{
    public int Id { get; set; }
}
