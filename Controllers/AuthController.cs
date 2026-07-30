using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSolution.Infrastructure.Identity;
using ProductSolution.ProductApplication.DTOs;

namespace ProductSolution.ProductAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtService;

    public AuthController(IJwtTokenService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginRequestDto request)
    {
        if (!ValidateUser(request, out string role))
        {
            return Unauthorized(new
            {
                Success = false,
                Message = "Invalid username or password."
            });
        }

        var accessToken = _jwtService.GenerateAccessToken(request.UserName, role);
        var refreshToken = _jwtService.GenerateRefreshToken();

        // Save Refresh Token
        RefreshTokenStore.Tokens[request.UserName] = refreshToken;

        return Ok(new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public IActionResult Refresh(RefreshTokenRequestDto request)
    {
        var user = RefreshTokenStore.Tokens
            .FirstOrDefault(x => x.Value == request.RefreshToken);

        if (string.IsNullOrEmpty(user.Key))
        {
            return Unauthorized(new
            {
                Success = false,
                Message = "Invalid refresh token."
            });
        }

        string role = GetRole(user.Key);

        var accessToken = _jwtService.GenerateAccessToken(user.Key, role);
        var refreshToken = _jwtService.GenerateRefreshToken();

        // Replace Refresh Token
        RefreshTokenStore.Tokens[user.Key] = refreshToken;

        return Ok(new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    private static bool ValidateUser(LoginRequestDto request, out string role)
    {
        role = string.Empty;

        if (request.UserName.Equals("admin", StringComparison.OrdinalIgnoreCase)
            && request.Password == "Admin@123")
        {
            role = "Admin";
            return true;
        }

        if (request.UserName.Equals("user", StringComparison.OrdinalIgnoreCase)
            && request.Password == "User@123")
        {
            role = "User";
            return true;
        }

        return false;
    }

    private static string GetRole(string username)
    {
        return username.Equals("admin", StringComparison.OrdinalIgnoreCase)
            ? "Admin"
            : "User";
    }
}