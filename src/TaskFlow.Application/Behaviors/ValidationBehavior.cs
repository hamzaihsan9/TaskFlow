using FluentValidation;
using MediatR;

namespace TaskFlow.Application.Behaviors;

/// <summary>
/// MediatR Pipeline Behavior that automatically validates requests
/// before they reach their handlers. This is a cross-cutting concern
/// that runs for every command/query, ensuring validation is consistent.
/// 
/// Think of it as middleware for MediatR — similar to how ASP.NET Core
/// middleware works for HTTP requests.
/// </summary>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f is not null)
                .ToList();

            if (failures.Any())
            {
                var errors = string.Join("; ", failures.Select(f => f.ErrorMessage));
                throw new Domain.Exceptions.BadRequestException($"Validation failed: {errors}");
            }
        }

        // If validation passes, continue to the handler
        return await next();
    }
}
