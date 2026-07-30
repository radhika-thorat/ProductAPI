namespace ProductSolution.ProductApplication.DTOs;

/// <summary>
/// Represents the request used to obtain a new access token
/// by providing a valid refresh token.
/// </summary>
public class RefreshTokenRequestDto
{
    /// <summary>
    /// Gets or sets the refresh token issued during authentication.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}