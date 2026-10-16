using FluentValidation.TestHelper;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Validators;
using Xunit;

namespace TaskFlow.UnitTests;

/// <summary>
/// Unit tests for FluentValidation rules on CreateTaskDto.
/// </summary>
public class CreateTaskValidatorTests
{
    private readonly CreateTaskValidator _validator;

    public CreateTaskValidatorTests()
    {
        _validator = new CreateTaskValidator();
    }

    [Fact]
    public void Validate_EmptyTitle_ShouldHaveValidationError()
    {
        // Arrange
        var model = new CreateTaskDto { Title = string.Empty, Priority = "High" };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title)
              .WithErrorMessage("Title is required.");
    }

    [Fact]
    public void Validate_InvalidPriority_ShouldHaveValidationError()
    {
        // Arrange
        var model = new CreateTaskDto { Title = "Valid Title", Priority = "Urgent" }; // Urgent is not allowed, must be Low/Medium/High/Critical

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Priority)
              .WithErrorMessage("Priority must be one of: Low, Medium, High, Critical.");
    }

    [Fact]
    public void Validate_PastDueDate_ShouldHaveValidationError()
    {
        // Arrange
        var model = new CreateTaskDto
        {
            Title = "Valid Title",
            Priority = "Medium",
            DueDate = DateTime.UtcNow.AddDays(-2)
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DueDate)
              .WithErrorMessage("Due date must be in the future.");
    }

    [Fact]
    public void Validate_ValidModel_ShouldNotHaveAnyErrors()
    {
        // Arrange
        var model = new CreateTaskDto
        {
            Title = "Implement clean architecture",
            Description = "Full stack C# sample",
            Priority = "Critical",
            DueDate = DateTime.UtcNow.AddDays(7)
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
