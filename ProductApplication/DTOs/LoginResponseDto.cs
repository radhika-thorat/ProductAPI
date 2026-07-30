namespace ProductSolution.ProductApplication.DTOs;

/// <summary>
/// Represents the authentication response returned after a successful login
/// or refresh token request.
/// </summary>
public class LoginResponseDto
{
    /// <summary>
    /// Gets or sets the JWT access token used to access protected API endpoints.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the refresh token used to generate a new access token
    /// when the current access token expires.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}