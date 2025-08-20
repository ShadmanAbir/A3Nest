using A3Nest.Presentation.Services;
using Microsoft.Extensions.Logging;

namespace A3Nest.Presentation;

public partial class App : Microsoft.Maui.Controls.Application
{
	private readonly IThemeService _themeService;
	private readonly IServiceProvider _serviceProvider;
	private readonly IErrorHandlingService _errorHandlingService;
	private readonly ILogger<App> _logger;

	public App(IThemeService themeService, IServiceProvider serviceProvider, IErrorHandlingService errorHandlingService, ILogger<App> logger)
	{
		_themeService = themeService;
		_serviceProvider = serviceProvider;
		_errorHandlingService = errorHandlingService;
		_logger = logger;
		
		InitializeComponent();
		
		// Initialize theme service
		_themeService.Initialize();
		
		// Log application startup
		_logger.LogInformation("A3Nest application starting up");
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		try
		{
			var shell = _serviceProvider.GetRequiredService<AppShell>();
			var window = new Window(shell);
			
			_logger.LogInformation("Main window created successfully");
			return window;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Failed to create main window");
			_errorHandlingService.HandleExceptionAsync(ex, "Window Creation");
			throw;
		}
	}

	protected override void OnStart()
	{
		base.OnStart();
		_logger.LogInformation("A3Nest application started");
	}

	protected override void OnSleep()
	{
		base.OnSleep();
		_logger.LogInformation("A3Nest application going to sleep");
	}

	protected override void OnResume()
	{
		base.OnResume();
		_logger.LogInformation("A3Nest application resumed");
	}
}