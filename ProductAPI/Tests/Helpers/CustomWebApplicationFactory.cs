using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests.Helpers;

/// <summary>
/// Represents a custom web application factory used for integration testing.
/// Inherits from <see cref="WebApplicationFactory{TProgram}"/> to create a test host.
/// </summary>
/// <typeparam name="TProgram">
/// The entry point class of the application under test.
/// </typeparam>
public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram>
    where TProgram : class
{
}