namespace ProductSolution.Infrastructure.Identity;

/// <summary>
/// Defines methods for generating JWT access tokens and refresh tokens.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates a JWT access token for the specified user.
    /// </summary>
    /// <param name="username">
    /// Username of the authenticated user.
    /// </param>
    /// <param name="role">
    /// Role assigned to the authenticated user.
    /// </param>
    /// <returns>
    /// A signed JWT access token.
    /// </returns>
    string GenerateAccessToken(string username, string role);

    /// <summary>
    /// Generates a cryptographically secure refresh token.
    /// </summary>
    /// <returns>
    /// A Base64 encoded refresh token.
    /// </returns>
    string GenerateRefreshToken();
}