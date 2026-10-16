using MediatR;

namespace TaskFlow.Application.Features.Categories.Commands.DeleteCategory;

/// <summary>
/// Command to delete a category by ID.
/// </summary>
public class DeleteCategoryCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
