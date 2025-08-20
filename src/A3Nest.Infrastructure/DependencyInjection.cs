using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using A3Nest.Application.Interfaces;
using A3Nest.Infrastructure.Data;
using A3Nest.Infrastructure.Repositories;
using A3Nest.Infrastructure.Hubs;
using A3Nest.Infrastructure.Search;
using A3Nest.Infrastructure.ExternalServices;

namespace A3Nest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Database
        services.AddDatabase(configuration);

        // Add Repository Pattern
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Add Repository Implementations for Application Services
        services.AddScoped<IPropertyService, PropertyRepository>();
        services.AddScoped<ITenantService, TenantRepository>();
        services.AddScoped<IMessageService, MessageRepository>();
        services.AddScoped<ITaskService, TaskRepository>();
        services.AddScoped<ICalendarService, CalendarRepository>();
        services.AddScoped<IOwnerPortalService, OwnerPortalRepository>();

        // Add SignalR services
        services.AddSignalR();

        // Add Elasticsearch services
        services.AddElasticsearch(configuration);
        services.AddScoped<IElasticsearchIndexer, ElasticsearchIndexer>();

        // Add External Service Adapters
        services.AddScoped<IEmailService, EmailServiceAdapter>();
        services.AddScoped<IPushNotificationService, PushNotificationServiceAdapter>();

        return services;
    }

    /// <summary>
    /// Extension method to configure SignalR hub endpoints
    /// This should be called from the Presentation layer in the Configure method
    /// </summary>
    /// <param name="app">The application builder</param>
    /// <returns>The application builder for chaining</returns>
    public static IApplicationBuilder UseSignalRHubs(this IApplicationBuilder app)
    {
        // Note: This method is intended to be used in ASP.NET Core applications
        // For MAUI applications, SignalR client configuration will be handled differently
        // This is provided as a placeholder for future server-side implementation
        throw new NotImplementedException("SignalR hub endpoint configuration will be implemented when server-side hosting is added");
    }}
