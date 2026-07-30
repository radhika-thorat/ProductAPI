using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductSolution.Infrastructure.Identity;
using ProductSolution.ProductAPI.Extensions;
using Serilog;
using System.Text;
using System.Text.Json;

/// <summary>
/// Entry point of the Product API application.
/// Configures services, middleware, authentication,
/// logging, API versioning, and Swagger.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

// Register JWT Token Service.
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();

#region Logging

// Configure Serilog logging.
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console()
        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day);
});

#endregion

#region Controllers

// Register API controllers.
builder.Services.AddControllers();

#endregion

#region API Versioning

// Configure API Versioning.
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

// Register Swagger services.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

#endregion

#region Application

// Register Application layer services.
builder.Services.AddApplication();

#endregion

#region Infrastructure

// Register Infrastructure layer services.
builder.Services.AddInfrastructure(builder.Configuration);

#endregion

#region AutoMapper

// Register AutoMapper.
builder.Services.AddAutoMapper(typeof(MappingProfile));

#endregion

#region FluentValidation

// Register FluentValidation validators.
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

#endregion

#region CORS

// Configure Cross-Origin Resource Sharing (CORS).
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

#endregion

#region JWT Authentication

// Configure JWT Authentication.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:SecretKey"];

        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException(
                "JWT SecretKey is missing from configuration.");
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
            // Executed when the JWT token is successfully validated.
            OnTokenValidated = context =>
            {
                Console.WriteLine("===== TOKEN VALID =====");

                foreach (var claim in context.Principal.Claims)
                {
                    Console.WriteLine($"{claim.Type} : {claim.Value}");
                }

                return Task.CompletedTask;
            },

            // Executed when the token is missing or invalid.
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

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(result));
            },

            // Executed when the token has expired.
            OnAuthenticationFailed = async context =>
            {
                if (context.Exception is SecurityTokenExpiredException)
                {
                    context.NoResult();

                    context.Response.StatusCode =
                        StatusCodes.Status401Unauthorized;

                    context.Response.ContentType = "application/json";

                    var result = new
                    {
                        StatusCode = 401,
                        Success = false,
                        Message = "Your JWT token has expired. Please login again."
                    };

                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(result));
                }
            },

            // Executed when the authenticated user is forbidden.
            OnForbidden = async context =>
            {
                context.Response.StatusCode =
                    StatusCodes.Status403Forbidden;

                context.Response.ContentType = "application/json";

                var result = new
                {
                    StatusCode = 403,
                    Success = false,
                    Message = "You do not have permission to access this resource."
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(result));
            }
        };
    });

// Configure Swagger with JWT Authentication.
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product API",
        Version = "v1"
    });

    // Configure JWT Bearer Authentication.
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token.\nExample: Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    // Apply JWT authentication globally.
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

// Register Authorization services.
builder.Services.AddAuthorization();

#endregion

// Build the application.
var app = builder.Build();

#region Middleware

// Enable Swagger in Development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Add security headers.
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["Referrer-Policy"] = "no-referrer";
    context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
    context.Response.Headers["Permissions-Policy"] =
        "geolocation=(), microphone=(), camera=()";

    context.Response.Headers["Content-Security-Policy"] =
        "default-src 'self'; object-src 'none'; frame-ancestors 'none'; base-uri 'self';";

    await next();
});

// Enable Authentication middleware.
app.UseAuthentication();

// Enable Authorization middleware.
app.UseAuthorization();

// Map API Controllers.
app.MapControllers();

#endregion

// Start the application.
app.Run();