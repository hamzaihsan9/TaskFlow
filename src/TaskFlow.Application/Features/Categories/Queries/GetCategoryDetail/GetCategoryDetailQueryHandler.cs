using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Categories.Queries.GetCategoryDetail;

/// <summary>
/// Handles retrieving a single category with task count.
/// </summary>
public class GetCategoryDetailQueryHandler : IRequestHandler<GetCategoryDetailQuery, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryDetailQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryWithTasksAsync(request.Id)
            ?? throw new NotFoundException(nameof(Category), request.Id);

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            TaskCount = category.Tasks?.Count ?? 0,
            CreatedAt = category.CreatedAt
        };
    }
}
