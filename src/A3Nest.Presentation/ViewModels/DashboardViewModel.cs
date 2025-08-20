using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using System.Collections.ObjectModel;

namespace A3Nest.Presentation.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    private readonly IPropertyService _propertyService;
    private readonly ITenantService _tenantService;
    private readonly ITaskService _taskService;
    private readonly IMessageService _messageService;
    private readonly ICalendarService _calendarService;

    public DashboardViewModel(
        IPropertyService propertyService,
        ITenantService tenantService,
        ITaskService taskService,
        IMessageService messageService,
        ICalendarService calendarService)
    {
        _propertyService = propertyService;
        _tenantService = tenantService;
        _taskService = taskService;
        _messageService = messageService;
        _calendarService = calendarService;
        
        Title = "Dashboard";
        
        // Initialize collections
        RecentProperties = new ObservableCollection<PropertyDto>();
        RecentTenants = new ObservableCollection<TenantDto>();
        PendingTasks = new ObservableCollection<TaskDto>();
        UnreadMessages = new ObservableCollection<MessageDto>();
        UpcomingEvents = new ObservableCollection<CalendarEventDto>();
    }

    [ObservableProperty]
    private int totalProperties;

    [ObservableProperty]
    private int totalTenants;

    [ObservableProperty]
    private int pendingTasksCount;

    [ObservableProperty]
    private int unreadMessagesCount;

    [ObservableProperty]
    private int upcomingEventsCount;

    [ObservableProperty]
    private decimal totalRevenue;

    [ObservableProperty]
    private decimal monthlyRevenue;

    [ObservableProperty]
    private int occupancyRate;

    public ObservableCollection<PropertyDto> RecentProperties { get; }
    public ObservableCollection<TenantDto> RecentTenants { get; }
    public ObservableCollection<TaskDto> PendingTasks { get; }
    public ObservableCollection<MessageDto> UnreadMessages { get; }
    public ObservableCollection<CalendarEventDto> UpcomingEvents { get; }

    public override async Task LoadAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            ClearError();

            // Load dashboard data - placeholder implementation
            await LoadDashboardSummaryAsync();
            await LoadRecentDataAsync();
        }
        catch (Exception ex)
        {
            SetError($"Failed to load dashboard: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task LoadDashboardSummaryAsync()
    {
        // Placeholder implementation - would call actual services
        await Task.Delay(100); // Simulate async operation
        
        TotalProperties = 25;
        TotalTenants = 48;
        PendingTasksCount = 7;
        UnreadMessagesCount = 3;
        UpcomingEventsCount = 5;
        TotalRevenue = 125000m;
        MonthlyRevenue = 15000m;
        OccupancyRate = 92;
    }

    private async Task LoadRecentDataAsync()
    {
        // Placeholder implementation - would call actual services
        await Task.Delay(100); // Simulate async operation
        
        // Clear existing data
        RecentProperties.Clear();
        RecentTenants.Clear();
        PendingTasks.Clear();
        UnreadMessages.Clear();
        UpcomingEvents.Clear();

        // Add sample data (in real implementation, would load from services)
        // This would be replaced with actual service calls like:
        // var properties = await _propertyService.GetPropertiesAsync();
        // foreach (var property in properties.Take(5))
        //     RecentProperties.Add(property);
    }

    [RelayCommand]
    private async Task NavigateToPropertiesAsync()
    {
        // Navigation logic placeholder
        await Shell.Current.GoToAsync("//properties");
    }

    [RelayCommand]
    private async Task NavigateToTenantsAsync()
    {
        // Navigation logic placeholder
        await Shell.Current.GoToAsync("//tenants");
    }

    [RelayCommand]
    private async Task NavigateToTasksAsync()
    {
        // Navigation logic placeholder
        await Shell.Current.GoToAsync("//tasks");
    }

    [RelayCommand]
    private async Task NavigateToMessagesAsync()
    {
        // Navigation logic placeholder
        await Shell.Current.GoToAsync("//messaging");
    }

    [RelayCommand]
    private async Task NavigateToCalendarAsync()
    {
        // Navigation logic placeholder
        await Shell.Current.GoToAsync("//calendar");
    }
}