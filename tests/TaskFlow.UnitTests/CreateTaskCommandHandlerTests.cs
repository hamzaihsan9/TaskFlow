using FluentAssertions;
using Moq;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Features.Tasks.Commands.CreateTask;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Interfaces;
using Xunit;

namespace TaskFlow.UnitTests;

/// <summary>
/// Unit tests for the CreateTaskCommandHandler.
/// Demonstrates how Clean Architecture makes testing easy —
/// we mock the repository interfaces and test the handler in isolation.
/// </summary>
public class CreateTaskCommandHandlerTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepo;
    private readonly Mock<ICategoryRepository> _mockCategoryRepo;
    private readonly CreateTaskCommandHandler _handler;

    public CreateTaskCommandHandlerTests()
    {
        _mockTaskRepo = new Mock<ITaskRepository>();
        _mockCategoryRepo = new Mock<ICategoryRepository>();
        _handler = new CreateTaskCommandHandler(_mockTaskRepo.Object, _mockCategoryRepo.Object);
    }

    [Fact]
    public async Task Handle_ValidTask_ReturnsCreatedTask()
    {
        // Arrange
        var dto = new CreateTaskDto
        {
            Title = "Write unit tests",
            Description = "Test the CQRS handlers",
            Priority = "High"
        };

        var command = new CreateTaskCommand { TaskDto = dto };

        var createdEntity = new TaskItem
        {
            Id = 1,
            Title = dto.Title,
            Description = dto.Description,
            Priority = Priority.High,
            Status = TaskItemStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        _mockTaskRepo
            .Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
            .ReturnsAsync(createdEntity);

        _mockTaskRepo
            .Setup(r => r.GetTaskWithCategoryAsync(1))
            .ReturnsAsync(createdEntity);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Write unit tests");
        result.Priority.Should().Be("High");
        result.Status.Should().Be("Pending");

        _mockTaskRepo.Verify(r => r.AddAsync(It.IsAny<TaskItem>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidCategory_ThrowsNotFoundException()
    {
        // Arrange
        var dto = new CreateTaskDto
        {
            Title = "Test task",
            Priority = "Medium",
            CategoryId = 999  // Non-existent category
        };

        var command = new CreateTaskCommand { TaskDto = dto };

        _mockCategoryRepo
            .Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Category?)null);

        // Act & Assert
        await Assert.ThrowsAsync<Domain.Exceptions.NotFoundException>(
            () => _handler.Handle(command, CancellationToken.None));
    }
}
