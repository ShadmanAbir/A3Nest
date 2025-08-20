using A3Nest.Presentation.Pages;

namespace A3Nest.Presentation;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
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
}
