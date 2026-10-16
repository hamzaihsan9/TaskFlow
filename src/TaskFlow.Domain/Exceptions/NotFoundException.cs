namespace TaskFlow.Domain.Exceptions;

/// <summary>
/// Exception thrown when a requested entity cannot be found in the data store.
/// This maps to an HTTP 404 Not Found response in the API layer.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"{name} with id ({key}) was not found.")
    {
    }
}
