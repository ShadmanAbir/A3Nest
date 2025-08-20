using A3Nest.Presentation.Services;
using A3Nest.Presentation.ViewModels;
using ResponsiveDeviceType = A3Nest.Presentation.Services.DeviceType;

namespace A3Nest.Presentation.Pages;

public partial class DashboardPage : ContentPage
{
    private readonly IResponsiveLayoutService _responsiveLayoutService;
    private readonly DashboardViewModel _viewModel;

    public DashboardPage(DashboardViewModel viewModel, IResponsiveLayoutService responsiveLayoutService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _responsiveLayoutService = responsiveLayoutService;
        
        // Subscribe to layout changes
        _responsiveLayoutService.LayoutChanged += OnLayoutChanged;
        
        // Set initial layout
        UpdateResponsiveLayout(_responsiveLayoutService.CurrentDeviceType);
    }

    private void OnLayoutChanged(object? sender, ResponsiveDeviceType deviceType)
    {
        MainThread.BeginInvokeOnMainThread(() => UpdateResponsiveLayout(deviceType));
    }

    private void UpdateResponsiveLayout(ResponsiveDeviceType deviceType)
    {
        // For now, just log the device type change
        // More complex responsive layout changes can be implemented later
        System.Diagnostics.Debug.WriteLine($"Device type changed to: {deviceType}");
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        // Load sample data when page appears
        await _viewModel.LoadAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        
        // Unsubscribe from layout changes
        if (_responsiveLayoutService != null)
        {
            _responsiveLayoutService.LayoutChanged -= OnLayoutChanged;
        }
    }
}