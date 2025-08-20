using Microsoft.Extensions.Logging;
using A3Nest.Application.Interfaces;
using A3Nest.Infrastructure.Data;

namespace A3Nest.Presentation.Services;

public class StartupService : IStartupService
{
    private readonly ILogger<StartupService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public StartupService(ILogger<StartupService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task InitializeAsync()
    {
        _logger.LogInformation("Starting application initialization");

        try
        {
            // Initialize database
            await _serviceProvider.InitializeDatabaseAsync();
            _logger.LogInformation("Database initialized successfully");

            // Validate services
            await ValidateServicesAsync();
            _logger.LogInformation("Service validation completed");

            // Seed sample data if needed
            await SeedDataAsync();
            _logger.LogInformation("Data seeding completed");

            _logger.LogInformation("Application initialization completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Application initialization failed");
            throw;
        }
    }

    public async Task SeedDataAsync()
    {
        try
        {
            // In a real application, you would check if data exists and seed if needed
            // For now, we'll just log that seeding is available
            _logger.LogInformation("Sample data seeding is available through ISampleDataService");
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Data seeding failed");
            throw;
        }
    }

    public async Task ValidateServicesAsync()
    {
        try
        {
            // Validate that critical services are available
            var propertyService = _serviceProvider.GetRequiredService<IPropertyService>();
            var tenantService = _serviceProvider.GetRequiredService<ITenantService>();
            var messageService = _serviceProvider.GetRequiredService<IMessageService>();
            var taskService = _serviceProvider.GetRequiredService<ITaskService>();
            var calendarService = _serviceProvider.GetRequiredService<ICalendarService>();
            var ownerPortalService = _serviceProvider.GetRequiredService<IOwnerPortalService>();

            var themeService = _serviceProvider.GetRequiredService<IThemeService>();
            var responsiveLayoutService = _serviceProvider.GetRequiredService<IResponsiveLayoutService>();
            var errorHandlingService = _serviceProvider.GetRequiredService<IErrorHandlingService>();
            var sampleDataService = _serviceProvider.GetRequiredService<ISampleDataService>();

            _logger.LogInformation("All required services are properly registered and available");
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Service validation failed");
            throw;
        }
    }
}