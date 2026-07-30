using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductSolution.ProductApplication.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProductSolution.Infrastructure.Identity;

/// <summary>
/// Provides JWT access token and refresh token generation.
/// </summary>
public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtTokenService"/> class.
    /// </summary>
    /// <param name="configuration">
    /// Application configuration containing JWT settings.
    /// </param>
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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
    public string GenerateAccessToken(string username, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Generates a cryptographically secure refresh token.
    /// </summary>
    /// <returns>
    /// A Base64 encoded refresh token.
    /// </returns>
    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}