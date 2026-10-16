namespace TaskFlow.Domain.Exceptions;

/// <summary>
/// Exception thrown when a request contains invalid data or violates business rules.
/// This maps to an HTTP 400 Bad Request response in the API layer.
/// </summary>
public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message)
    {
    }
}
