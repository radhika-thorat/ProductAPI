namespace ProductSolution.Infrastructure.Identity;

/// <summary>
/// Represents a refresh token used to generate a new JWT access token.
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Gets or sets the refresh token value.
    /// </summary>
    public string Token { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the expiration date and time of the refresh token.
    /// </summary>
    public DateTime Expiration { get; set; }

    /// <summary>
    /// Gets a value indicating whether the refresh token has expired.
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= Expiration;
}

/// <summary>
/// Provides an in-memory store for refresh tokens.
/// </summary>
public static class RefreshTokenStore
{
    /// <summary>
    /// Stores refresh tokens using the username as the key
    /// and the refresh token as the value.
    /// </summary>
    public static Dictionary<string, string> Tokens = new();
}