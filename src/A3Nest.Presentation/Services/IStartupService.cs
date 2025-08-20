namespace A3Nest.Presentation.Services;

public interface IStartupService
{
    /// <summary>
    /// Performs application startup initialization
    /// </summary>
    Task InitializeAsync();

    /// <summary>
    /// Performs database seeding with sample data if needed
    /// </summary>
    Task SeedDataAsync();

    /// <summary>
    /// Validates that all required services are properly configured
    /// </summary>
    Task ValidateServicesAsync();
}