using Asp.Versioning;
using FluentValidation;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductAPI.Extensions;
using Serilog;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();

#region Logging

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console()
        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day);
});

#endregion

#region Controllers

builder.Services.AddControllers();

#endregion

#region API Versioning

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

#endregion

#region Swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

#endregion

#region Application

builder.Services.AddApplication();

#endregion

#region Infrastructure

builder.Services.AddInfrastructure(builder.Configuration);

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(typeof(MappingProfile));

#endregion

#region FluentValidation

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("https://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

#region JWT Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:SecretKey"];

        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException("JWT SecretKey is missing from configuration.");
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };

        options.Events = new JwtBearerEvents
        {
            // No token or invalid token
            OnChallenge = async context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var result = new
                {
                    StatusCode = 401,
                    Success = false,
                    Message = "Authorization required. Please login and provide a valid JWT token."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            },

            // Token expired
            OnAuthenticationFailed = async context =>
            {
                if (context.Exception is SecurityTokenExpiredException)
                {
                    context.NoResult();

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var result = new
                    {
                        StatusCode = 401,
                        Success = false,
                        Message = "Your JWT token has expired. Please login again."
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(result));
                }
            },

            // Valid token but insufficient permissions
            OnForbidden = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var result = new
                {
                    StatusCode = 403,
                    Success = false,
                    Message = "You do not have permission to access this resource."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product API",
        Version = "v1"
    });

    // JWT Authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token.\nExample: Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthorization();

#endregion

var app = builder.Build();

#region Middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); //HTTPS & TLS 1.2+

app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["Referrer-Policy"] = "no-referrer";
    context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
    context.Response.Headers["Permissions-Policy"] = "geolocation=(), microphone=(), camera=()";

    context.Response.Headers["Content-Security-Policy"] =
        "default-src 'self'; object-src 'none'; frame-ancestors 'none'; base-uri 'self';";

    await next();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();