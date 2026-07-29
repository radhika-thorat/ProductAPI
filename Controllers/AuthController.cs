using Asp.Versioning;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApplication.DTOs;
using ProductApplication.Interfaces;

namespace ProductAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly Infrastructure.Identity.IJwtTokenService _jwtService;

    public AuthController(Infrastructure.Identity.IJwtTokenService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginRequestDto request)
    {
        string role;

        if (request.UserName == "admin" &&
            request.Password == "Admin@123")
        {
            role = "Admin";
        }
        else if (request.UserName == "user" &&
                 request.Password == "User@123")
        {
            role = "User";
        }
        else
        {
            return Unauthorized(new
            {
                Message = "Invalid username or password."
            });
        }

        var accessToken = _jwtService.GenerateAccessToken(
            request.UserName,
            role);

        var refreshToken = _jwtService.GenerateRefreshToken();

        return Ok(new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public IActionResult Refresh(RefreshTokenRequestDto request)
    {
        if (!RefreshTokenStore.Tokens.ContainsValue(request.RefreshToken))
            return Unauthorized();

        var accessToken =
            _jwtService.GenerateAccessToken("admin", "Admin");

        var refreshToken =
            _jwtService.GenerateRefreshToken();

        RefreshTokenStore.Tokens["admin"] = refreshToken;

        return Ok(new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
}