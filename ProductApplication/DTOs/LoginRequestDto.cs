namespace ProductSolution.ProductApplication.DTOs;

/// <summary>
/// Represents the user credentials required for authentication.
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}