using A3Nest.Presentation.Pages;
using A3Nest.Presentation.Services;

namespace A3Nest.Presentation;

public partial class AppShell : Shell
{
    private readonly IResponsiveLayoutService _responsiveLayoutService;
    
    public AppShell(IResponsiveLayoutService responsiveLayoutService)
    {
        _responsiveLayoutService = responsiveLayoutService;
        InitializeComponent();
        RegisterRoutes();
        ConfigureResponsiveNavigation();
    }

    private void RegisterRoutes()
    {
        // Register routes for navigation
        Routing.RegisterRoute("dashboard", typeof(DashboardPage));
        Routing.RegisterRoute("properties", typeof(PropertiesPage));
        Routing.RegisterRoute("tenants", typeof(TenantsPage));
        Routing.RegisterRoute("leaseapplications", typeof(LeaseApplicationsPage));
        Routing.RegisterRoute("ownerportal", typeof(OwnerPortalPage));
        Routing.RegisterRoute("messaging", typeof(MessagingPage));
        Routing.RegisterRoute("calendar", typeof(CalendarPage));
        Routing.RegisterRoute("settings", typeof(SettingsPage));
        Routing.RegisterRoute("guestview", typeof(GuestViewPage));

        // Register mobile-specific routes
        Routing.RegisterRoute("dashboard_mobile", typeof(DashboardPage));
        Routing.RegisterRoute("properties_mobile", typeof(PropertiesPage));
        Routing.RegisterRoute("tenants_mobile", typeof(TenantsPage));
        Routing.RegisterRoute("messaging_mobile", typeof(MessagingPage));
        Routing.RegisterRoute("settings_mobile", typeof(SettingsPage));
    }
    
    private void ConfigureResponsiveNavigation()
    {
        _responsiveLayoutService.LayoutChanged += OnLayoutChanged;
        UpdateNavigationForCurrentLayout();
    }
    
    private void OnLayoutChanged(object? sender, Services.DeviceType deviceType)
    {
        UpdateNavigationForCurrentLayout();
    }
    
    private void UpdateNavigationForCurrentLayout()
    {
        // Configure navigation based on device type
        if (_responsiveLayoutService.ShouldUseMobileNavigation)
        {
            // Mobile: Use TabBar navigation
            FlyoutBehavior = FlyoutBehavior.Disabled;
            MobileTabBar.IsVisible = true;
        }
        else
        {
            // Desktop/Tablet: Use Flyout navigation
            FlyoutBehavior = FlyoutBehavior.Flyout;
            MobileTabBar.IsVisible = false;
        }
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _responsiveLayoutService.Initialize();
    }
}
