using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Categories.Queries.GetAllCategories;

/// <summary>
/// Query to retrieve all categories.
/// </summary>
public class GetAllCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>
{
}
