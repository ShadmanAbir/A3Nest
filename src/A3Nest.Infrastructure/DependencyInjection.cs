using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using A3Nest.Application.Interfaces;
using A3Nest.Infrastructure.Data;
using A3Nest.Infrastructure.Repositories;

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

        // Additional infrastructure services will be added here in future tasks
        // - SignalR hubs
        // - Elasticsearch services
        // - External service adapters

        return services;
    }
}