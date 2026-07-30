namespace ProductSolution.ProductDomain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested resource cannot be found.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class
    /// with the specified error message.
    /// </summary>
    /// <param name="message">The error message that describes the exception.</param>
    public NotFoundException(string message)
        : base(message)
    {
    }
}