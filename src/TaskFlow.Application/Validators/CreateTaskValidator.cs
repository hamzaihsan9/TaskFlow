using FluentValidation;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Validators;

/// <summary>
/// Validates CreateTaskDto input using FluentValidation.
/// These rules run before the command handler, providing
/// clear, declarative validation logic.
/// </summary>
public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(t => t.Description)
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.")
            .When(t => t.Description is not null);

        RuleFor(t => t.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .Must(BeAValidPriority).WithMessage("Priority must be one of: Low, Medium, High, Critical.");

        RuleFor(t => t.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.")
            .When(t => t.DueDate.HasValue);
    }

    private static bool BeAValidPriority(string priority)
    {
        return priority is "Low" or "Medium" or "High" or "Critical";
    }
}
