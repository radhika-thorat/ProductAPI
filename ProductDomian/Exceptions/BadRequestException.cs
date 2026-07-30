namespace ProductSolution.ProductDomain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a bad request is encountered.
/// </summary>
public class BadRequestException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BadRequestException"/> class
    /// with the specified error message.
    /// </summary>
    /// <param name="message">The error message that describes the exception.</param>
    public BadRequestException(string message)
        : base(message)
    {
    }
}