using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Categories.Commands.CreateCategory;

/// <summary>
/// Handles creating a new category with uniqueness validation.
/// </summary>
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Ensure category name is unique
        if (await _categoryRepository.CategoryExistsAsync(request.CategoryDto.Name))
            throw new BadRequestException($"A category with name '{request.CategoryDto.Name}' already exists.");

        var category = new Category
        {
            Name = request.CategoryDto.Name,
            Description = request.CategoryDto.Description
        };

        var created = await _categoryRepository.AddAsync(category);

        return new CategoryDto
        {
            Id = created.Id,
            Name = created.Name,
            Description = created.Description,
            TaskCount = 0,
            CreatedAt = created.CreatedAt
        };
    }
}
