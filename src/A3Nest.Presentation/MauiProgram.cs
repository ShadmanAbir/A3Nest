using Microsoft.Extensions.Logging;
using A3Nest.Infrastructure;
using A3Nest.Application.Services;
using A3Nest.Application.Interfaces;
using A3Nest.Presentation.ViewModels;
using A3Nest.Presentation.Pages;

namespace A3Nest.Presentation;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Add Infrastructure services
		builder.Services.AddInfrastructure(builder.Configuration);

		// Add Application services
		builder.Services.AddScoped<IPropertyService, PropertyService>();
		builder.Services.AddScoped<ITenantService, TenantService>();
		builder.Services.AddScoped<IMessageService, MessageService>();
		builder.Services.AddScoped<ITaskService, TaskService>();
		builder.Services.AddScoped<ICalendarService, CalendarService>();
		builder.Services.AddScoped<IOwnerPortalService, OwnerPortalService>();

		// Register ViewModels
		builder.Services.AddTransient<DashboardViewModel>();
		builder.Services.AddTransient<PropertiesViewModel>();
		builder.Services.AddTransient<TenantsViewModel>();
		builder.Services.AddTransient<LeaseApplicationsViewModel>();
		builder.Services.AddTransient<OwnerPortalViewModel>();
		builder.Services.AddTransient<MessagingViewModel>();
		builder.Services.AddTransient<CalendarViewModel>();
		builder.Services.AddTransient<SettingsViewModel>();
		builder.Services.AddTransient<GuestViewViewModel>();

		// Register Pages
		builder.Services.AddTransient<DashboardPage>();
		builder.Services.AddTransient<PropertiesPage>();
		builder.Services.AddTransient<TenantsPage>();
		builder.Services.AddTransient<LeaseApplicationsPage>();
		builder.Services.AddTransient<OwnerPortalPage>();
		builder.Services.AddTransient<MessagingPage>();
		builder.Services.AddTransient<CalendarPage>();
		builder.Services.AddTransient<SettingsPage>();
		builder.Services.AddTransient<GuestViewPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
