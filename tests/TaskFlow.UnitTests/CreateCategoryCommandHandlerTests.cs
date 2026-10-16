using FluentAssertions;
using Moq;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Features.Categories.Commands.CreateCategory;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces;
using Xunit;

namespace TaskFlow.UnitTests;

/// <summary>
/// Unit tests for CreateCategoryCommandHandler.
/// Verifies business validation such as unique category name check.
/// </summary>
public class CreateCategoryCommandHandlerTests
{
    private readonly Mock<ICategoryRepository> _mockCategoryRepo;
    private readonly CreateCategoryCommandHandler _handler;

    public CreateCategoryCommandHandlerTests()
    {
        _mockCategoryRepo = new Mock<ICategoryRepository>();
        _handler = new CreateCategoryCommandHandler(_mockCategoryRepo.Object);
    }

    [Fact]
    public async Task Handle_UniqueCategory_ReturnsCreatedCategory()
    {
        // Arrange
        var dto = new CreateCategoryDto
        {
            Name = "DevOps",
            Description = "Tasks related to CI/CD and deployment"
        };
        var command = new CreateCategoryCommand { CategoryDto = dto };

        _mockCategoryRepo
            .Setup(r => r.CategoryExistsAsync(dto.Name))
            .ReturnsAsync(false);

        _mockCategoryRepo
            .Setup(r => r.AddAsync(It.IsAny<Category>()))
            .ReturnsAsync(new Category
            {
                Id = 10,
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(10);
        result.Name.Should().Be("DevOps");
        result.Description.Should().Be("Tasks related to CI/CD and deployment");

        _mockCategoryRepo.Verify(r => r.AddAsync(It.IsAny<Category>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DuplicateCategory_ThrowsBadRequestException()
    {
        // Arrange
        var dto = new CreateCategoryDto { Name = "ExistingCategory" };
        var command = new CreateCategoryCommand { CategoryDto = dto };

        _mockCategoryRepo
            .Setup(r => r.CategoryExistsAsync(dto.Name))
            .ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(
            () => _handler.Handle(command, CancellationToken.None));

        _mockCategoryRepo.Verify(r => r.AddAsync(It.IsAny<Category>()), Times.Never);
    }
}
