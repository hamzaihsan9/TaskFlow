using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Categories.Commands.CreateCategory;

/// <summary>
/// Command to create a new category.
/// </summary>
public class CreateCategoryCommand : IRequest<CategoryDto>
{
    public CreateCategoryDto CategoryDto { get; set; } = null!;
}
