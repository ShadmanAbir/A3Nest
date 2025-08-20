using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Presentation.Services;
using System.Collections.ObjectModel;

namespace A3Nest.Presentation.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    private readonly IPropertyService _propertyService;
    private readonly ITenantService _tenantService;
    private readonly ITaskService _taskService;
    private readonly IMessageService _messageService;
    private readonly ICalendarService _calendarService;
    private readonly ISampleDataService _sampleDataService;

    public DashboardViewModel(
        IPropertyService propertyService,
        ITenantService tenantService,
        ITaskService taskService,
        IMessageService messageService,
        ICalendarService calendarService,
        ISampleDataService sampleDataService)
    {
        _propertyService = propertyService;
        _tenantService = tenantService;
        _taskService = taskService;
        _messageService = messageService;
        _calendarService = calendarService;
        _sampleDataService = sampleDataService;
        
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
        // Load sample data for dashboard summary
        var properties = await _sampleDataService.GetSamplePropertiesAsync();
        var tenants = await _sampleDataService.GetSampleTenantsAsync();
        var tasks = await _sampleDataService.GetSampleTasksAsync();
        var messages = await _sampleDataService.GetSampleMessagesAsync();
        var events = await _sampleDataService.GetSampleCalendarEventsAsync();
        var financialSummary = await _sampleDataService.GetSampleOwnerFinancialSummaryAsync();
        
        TotalProperties = properties.Count();
        TotalTenants = tenants.Count();
        PendingTasksCount = tasks.Count(t => t.Status != A3Nest.Domain.Enums.TaskStatus.Completed);
        UnreadMessagesCount = messages.Count(m => !m.IsRead);
        UpcomingEventsCount = events.Count(e => e.StartDate >= DateTime.Now);
        TotalRevenue = (decimal)financialSummary.TotalIncome.Amount;
        MonthlyRevenue = (decimal)financialSummary.NetIncome.Amount;
        OccupancyRate = 92; // Calculated based on sample data
    }

    private async Task LoadRecentDataAsync()
    {
        // Clear existing data
        RecentProperties.Clear();
        RecentTenants.Clear();
        PendingTasks.Clear();
        UnreadMessages.Clear();
        UpcomingEvents.Clear();

        // Load sample data from service
        var properties = await _sampleDataService.GetSamplePropertiesAsync();
        var tenants = await _sampleDataService.GetSampleTenantsAsync();
        var tasks = await _sampleDataService.GetSampleTasksAsync();
        var messages = await _sampleDataService.GetSampleMessagesAsync();
        var events = await _sampleDataService.GetSampleCalendarEventsAsync();

        // Add recent properties (limit to 5)
        foreach (var property in properties.Take(5))
        {
            RecentProperties.Add(property);
        }

        // Add recent tenants (limit to 5)
        foreach (var tenant in tenants.Take(5))
        {
            RecentTenants.Add(tenant);
        }

        // Add pending tasks (limit to 5)
        foreach (var task in tasks.Where(t => t.Status != A3Nest.Domain.Enums.TaskStatus.Completed).Take(5))
        {
            PendingTasks.Add(task);
        }

        // Add unread messages (limit to 5)
        foreach (var message in messages.Where(m => !m.IsRead).Take(5))
        {
            UnreadMessages.Add(message);
        }

        // Add upcoming events (limit to 5)
        foreach (var calendarEvent in events.Where(e => e.StartDate >= DateTime.Now).Take(5))
        {
            UpcomingEvents.Add(calendarEvent);
        }
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