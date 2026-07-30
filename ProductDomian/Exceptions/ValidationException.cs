namespace ProductSolution.ProductDomain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when one or more validation errors occur.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class
    /// with the specified validation error message.
    /// </summary>
    /// <param name="message">The validation error message.</param>
    public ValidationException(string message)
        : base(message)
    {
    }
}