using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using A3Nest.Infrastructure;
using A3Nest.Application.Services;
using A3Nest.Application.Interfaces;
using A3Nest.Presentation.ViewModels;
using A3Nest.Presentation.Pages;
using A3Nest.Presentation.Services;
using A3Nest.Infrastructure.Data;

#if ANDROID
using Android.Runtime;
#endif

#if IOS
using Foundation;
#endif

namespace A3Nest.Presentation;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
        // Configure MAUI App
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Add Configuration
        ConfigureAppSettings(builder);

        // Configure Logging
        ConfigureLogging(builder);

        // Add Infrastructure Layer Services
        builder.Services.AddInfrastructure(builder.Configuration);

        // Add Application Layer Services
        ConfigureApplicationServices(builder.Services);

        // Add Presentation Layer Services
        ConfigurePresentationServices(builder.Services);

        // Register ViewModels with proper lifetime management
        ConfigureViewModels(builder.Services);

        // Register Pages with proper lifetime management
        ConfigurePages(builder.Services);

        // Register Shell and Navigation
        builder.Services.AddSingleton<AppShell>();

        var app = builder.Build();

        // Initialize services that require startup configuration
        InitializeServices(app.Services);

        return app;
    }

    private static void ConfigureAppSettings(MauiAppBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("A3Nest.Presentation.appsettings.json");
        
        if (stream != null)
        {
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            builder.Configuration.AddConfiguration(config);
        }
    }

    private static void ConfigureLogging(MauiAppBuilder builder)
    {
        // Configure logging based on build configuration
#if DEBUG
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
#else
        builder.Logging.SetMinimumLevel(LogLevel.Information);
#endif

        // Add console logging for all platforms
        builder.Logging.AddConsole();

        // Configure specific log levels
        builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
        builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
        builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
        builder.Logging.AddFilter("A3Nest", LogLevel.Information);
    }

    private static void ConfigureApplicationServices(IServiceCollection services)
    {
        // Application Services - Scoped lifetime for request-based operations
        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ICalendarService, CalendarService>();
        services.AddScoped<IOwnerPortalService, OwnerPortalService>();
    }

    private static void ConfigurePresentationServices(IServiceCollection services)
    {
        // Presentation Services - Singleton lifetime for app-wide state management
        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<IResponsiveLayoutService, ResponsiveLayoutService>();
        services.AddSingleton<IErrorHandlingService, ErrorHandlingService>();
        services.AddSingleton<ISampleDataService, SampleDataService>();
        services.AddSingleton<IStartupService, StartupService>();
        services.AddSingleton<IBrandingService, BrandingService>();
    }

    private static void ConfigureViewModels(IServiceCollection services)
    {
        // ViewModels - Transient lifetime for fresh instances per navigation
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<PropertiesViewModel>();
        services.AddTransient<TenantsViewModel>();
        services.AddTransient<LeaseApplicationsViewModel>();
        services.AddTransient<OwnerPortalViewModel>();
        services.AddTransient<MessagingViewModel>();
        services.AddTransient<CalendarViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<GuestViewViewModel>();
    }

    private static void ConfigurePages(IServiceCollection services)
    {
        // Pages - Transient lifetime for fresh instances per navigation
        services.AddTransient<DashboardPage>();
        services.AddTransient<PropertiesPage>();
        services.AddTransient<TenantsPage>();
        services.AddTransient<LeaseApplicationsPage>();
        services.AddTransient<OwnerPortalPage>();
        services.AddTransient<MessagingPage>();
        services.AddTransient<CalendarPage>();
        services.AddTransient<SettingsPage>();
        services.AddTransient<GuestViewPage>();
    }

    private static void InitializeServices(IServiceProvider services)
    {
        try
        {
            // Initialize theme service
            var themeService = services.GetRequiredService<IThemeService>();
            themeService.Initialize();

            // Initialize responsive layout service
            var responsiveLayoutService = services.GetRequiredService<IResponsiveLayoutService>();
            responsiveLayoutService.Initialize();

            // Initialize database (async operation handled in background)
            Task.Run(async () =>
            {
                try
                {
                    await services.InitializeDatabaseAsync();
                }
                catch (Exception ex)
                {
                    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger("MauiProgram");
                    logger.LogError(ex, "Failed to initialize database during startup");
                }
            });

            // Set up global exception handling
            SetupGlobalExceptionHandling(services);
        }
        catch (Exception ex)
        {
            // Log startup errors
            var loggerFactory = services.GetService<ILoggerFactory>();
            var logger = loggerFactory?.CreateLogger("MauiProgram");
            logger?.LogError(ex, "Error occurred during service initialization");
            throw;
        }
    }

    private static void SetupGlobalExceptionHandling(IServiceProvider services)
    {
        var errorHandler = services.GetRequiredService<IErrorHandlingService>();

        // Handle unhandled exceptions
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            if (e.ExceptionObject is Exception exception)
            {
                errorHandler.LogError(exception, "Unhandled AppDomain Exception");
            }
        };

        // Handle unobserved task exceptions
        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            errorHandler.LogError(e.Exception, "Unobserved Task Exception");
            e.SetObserved(); // Prevent the process from terminating
        };

        // Platform-specific exception handling
#if ANDROID
        AndroidEnvironment.UnhandledExceptionRaiser += (sender, e) =>
        {
            errorHandler.LogError(e.Exception, "Android Unhandled Exception");
        };
#endif

#if IOS
        // iOS exception handling would be implemented here if needed
#endif
    }
}
