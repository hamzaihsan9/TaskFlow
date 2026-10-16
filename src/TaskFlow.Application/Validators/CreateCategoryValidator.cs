using FluentValidation;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Validators;

/// <summary>
/// Validates CreateCategoryDto input.
/// </summary>
public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

        RuleFor(c => c.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
            .When(c => c.Description is not null);
    }
}
