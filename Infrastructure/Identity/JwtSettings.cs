namespace Infrastructure.Identity;

/// <summary>
/// Represents JWT configuration settings used for authentication.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Configuration section name in appsettings.json.
    /// </summary>
    public const string SectionName = "Jwt";

    /// <summary>
    /// Gets or sets the JWT token issuer.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the JWT token audience.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the secret key used to sign JWT tokens.
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the access token expiration time in minutes.
    /// </summary>
    public int ExpiryMinutes { get; set; }
}