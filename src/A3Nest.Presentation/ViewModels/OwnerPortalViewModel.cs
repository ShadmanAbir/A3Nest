using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using System.Collections.ObjectModel;

namespace A3Nest.Presentation.ViewModels;

public partial class OwnerPortalViewModel : BaseViewModel
{
    private readonly IOwnerPortalService _ownerPortalService;
    private readonly IPropertyService _propertyService;
    private readonly ITenantService _tenantService;

    public OwnerPortalViewModel(
        IOwnerPortalService ownerPortalService,
        IPropertyService propertyService,
        ITenantService tenantService)
    {
        _ownerPortalService = ownerPortalService;
        _propertyService = propertyService;
        _tenantService = tenantService;
        Title = "Owner Portal";
        
        OwnerProperties = new ObservableCollection<PropertyDto>();
        OwnerTenants = new ObservableCollection<TenantDto>();
        OwnerLeaseApplications = new ObservableCollection<LeaseApplicationDto>();
        OwnerReports = new ObservableCollection<ReportDto>();
        OwnerTasks = new ObservableCollection<TaskDto>();
        OwnerMessages = new ObservableCollection<MessageDto>();
    }

    [ObservableProperty]
    private int currentOwnerId = 1; // Placeholder - would come from authentication

    [ObservableProperty]
    private OwnerDashboardDto? ownerDashboard;

    [ObservableProperty]
    private OwnerFinancialSummaryDto? financialSummary;

    [ObservableProperty]
    private string selectedTimeRange = "This Month";

    [ObservableProperty]
    private DateTime financialStartDate = DateTime.Now.AddMonths(-1);

    [ObservableProperty]
    private DateTime financialEndDate = DateTime.Now;

    [ObservableProperty]
    private string selectedView = "Dashboard";

    [ObservableProperty]
    private bool isLoadingFinancials;

    public ObservableCollection<PropertyDto> OwnerProperties { get; }
    public ObservableCollection<TenantDto> OwnerTenants { get; }
    public ObservableCollection<LeaseApplicationDto> OwnerLeaseApplications { get; }
    public ObservableCollection<ReportDto> OwnerReports { get; }
    public ObservableCollection<TaskDto> OwnerTasks { get; }
    public ObservableCollection<MessageDto> OwnerMessages { get; }

    public List<string> TimeRangeOptions { get; } = new()
    {
        "This Month",
        "Last Month",
        "This Quarter",
        "Last Quarter",
        "This Year",
        "Last Year",
        "Custom"
    };

    public List<string> ViewOptions { get; } = new()
    {
        "Dashboard",
        "Properties",
        "Tenants",
        "Applications",
        "Reports",
        "Tasks",
        "Messages"
    };

    public override async Task LoadAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            ClearError();

            // Load owner dashboard data
            await LoadOwnerDashboardAsync();
            
            // Load data based on selected view
            await LoadViewDataAsync();
        }
        catch (Exception ex)
        {
            SetError($"Failed to load owner portal data: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task LoadFinancialSummaryAsync()
    {
        if (IsLoadingFinancials) return;

        try
        {
            IsLoadingFinancials = true;
            ClearError();

            // Placeholder implementation - would call actual service
            await Task.Delay(100); // Simulate async operation
            
            // In real implementation:
            // FinancialSummary = await _ownerPortalService.GetOwnerFinancialSummaryAsync(
            //     CurrentOwnerId, FinancialStartDate, FinancialEndDate);
        }
        catch (Exception ex)
        {
            SetError($"Failed to load financial summary: {ex.Message}");
        }
        finally
        {
            IsLoadingFinancials = false;
        }
    }

    [RelayCommand]
    private async Task RefreshViewAsync()
    {
        await LoadViewDataAsync();
    }

    [RelayCommand]
    private async Task ViewPropertyDetailsAsync(PropertyDto? property)
    {
        if (property == null) return;
        
        // Navigation to property details page placeholder
        await Shell.Current.GoToAsync($"//properties/details?id={property.Id}");
    }

    [RelayCommand]
    private async Task ViewTenantDetailsAsync(TenantDto? tenant)
    {
        if (tenant == null) return;
        
        // Navigation to tenant details page placeholder
        await Shell.Current.GoToAsync($"//tenants/details?id={tenant.Id}");
    }

    [RelayCommand]
    private async Task ViewApplicationDetailsAsync(LeaseApplicationDto? application)
    {
        if (application == null) return;
        
        // Navigation to application details page placeholder
        await Shell.Current.GoToAsync($"//leaseapplications/details?id={application.Id}");
    }

    [RelayCommand]
    private async Task ViewReportDetailsAsync(ReportDto? report)
    {
        if (report == null) return;
        
        // Navigation to report details page placeholder
        await Shell.Current.GoToAsync($"//reports/details?id={report.Id}");
    }

    [RelayCommand]
    private async Task ViewTaskDetailsAsync(TaskDto? task)
    {
        if (task == null) return;
        
        // Navigation to task details page placeholder
        await Shell.Current.GoToAsync($"//tasks/details?id={task.Id}");
    }

    [RelayCommand]
    private async Task ViewMessageDetailsAsync(MessageDto? message)
    {
        if (message == null) return;
        
        // Navigation to message details page placeholder
        await Shell.Current.GoToAsync($"//messaging/details?id={message.Id}");
    }

    partial void OnSelectedTimeRangeChanged(string value)
    {
        UpdateDateRangeFromSelection();
        _ = LoadFinancialSummaryAsync();
    }

    partial void OnSelectedViewChanged(string value)
    {
        _ = LoadViewDataAsync();
    }

    partial void OnFinancialStartDateChanged(DateTime value)
    {
        if (SelectedTimeRange == "Custom")
        {
            _ = LoadFinancialSummaryAsync();
        }
    }

    partial void OnFinancialEndDateChanged(DateTime value)
    {
        if (SelectedTimeRange == "Custom")
        {
            _ = LoadFinancialSummaryAsync();
        }
    }

    private async Task LoadOwnerDashboardAsync()
    {
        // Placeholder implementation - would call actual service
        await Task.Delay(100); // Simulate async operation
        
        // In real implementation:
        // OwnerDashboard = await _ownerPortalService.GetOwnerDashboardAsync(CurrentOwnerId);
    }

    private async Task LoadViewDataAsync()
    {
        switch (SelectedView)
        {
            case "Properties":
                await LoadOwnerPropertiesAsync();
                break;
            case "Tenants":
                await LoadOwnerTenantsAsync();
                break;
            case "Applications":
                await LoadOwnerLeaseApplicationsAsync();
                break;
            case "Reports":
                await LoadOwnerReportsAsync();
                break;
            case "Tasks":
                await LoadOwnerTasksAsync();
                break;
            case "Messages":
                await LoadOwnerMessagesAsync();
                break;
            case "Dashboard":
            default:
                await LoadFinancialSummaryAsync();
                break;
        }
    }

    private async Task LoadOwnerPropertiesAsync()
    {
        // Placeholder implementation - would call actual service
        await Task.Delay(100); // Simulate async operation
        
        OwnerProperties.Clear();
        
        // In real implementation:
        // var properties = await _ownerPortalService.GetOwnerPropertiesAsync(CurrentOwnerId);
        // foreach (var property in properties)
        //     OwnerProperties.Add(property);
    }

    private async Task LoadOwnerTenantsAsync()
    {
        // Placeholder implementation - would call actual service
        await Task.Delay(100); // Simulate async operation
        
        OwnerTenants.Clear();
        
        // In real implementation:
        // var tenants = await _ownerPortalService.GetOwnerTenantsAsync(CurrentOwnerId);
        // foreach (var tenant in tenants)
        //     OwnerTenants.Add(tenant);
    }

    private async Task LoadOwnerLeaseApplicationsAsync()
    {
        // Placeholder implementation - would call actual service
        await Task.Delay(100); // Simulate async operation
        
        OwnerLeaseApplications.Clear();
        
        // In real implementation:
        // var applications = await _ownerPortalService.GetOwnerLeaseApplicationsAsync(CurrentOwnerId);
        // foreach (var application in applications)
        //     OwnerLeaseApplications.Add(application);
    }

    private async Task LoadOwnerReportsAsync()
    {
        // Placeholder implementation - would call actual service
        await Task.Delay(100); // Simulate async operation
        
        OwnerReports.Clear();
        
        // In real implementation:
        // var reports = await _ownerPortalService.GetOwnerReportsAsync(CurrentOwnerId);
        // foreach (var report in reports)
        //     OwnerReports.Add(report);
    }

    private async Task LoadOwnerTasksAsync()
    {
        // Placeholder implementation - would call actual service
        await Task.Delay(100); // Simulate async operation
        
        OwnerTasks.Clear();
        
        // In real implementation:
        // var tasks = await _ownerPortalService.GetOwnerTasksAsync(CurrentOwnerId);
        // foreach (var task in tasks)
        //     OwnerTasks.Add(task);
    }

    private async Task LoadOwnerMessagesAsync()
    {
        // Placeholder implementation - would call actual service
        await Task.Delay(100); // Simulate async operation
        
        OwnerMessages.Clear();
        
        // In real implementation:
        // var messages = await _ownerPortalService.GetOwnerMessagesAsync(CurrentOwnerId);
        // foreach (var message in messages)
        //     OwnerMessages.Add(message);
    }

    private void UpdateDateRangeFromSelection()
    {
        var now = DateTime.Now;
        
        switch (SelectedTimeRange)
        {
            case "This Month":
                FinancialStartDate = new DateTime(now.Year, now.Month, 1);
                FinancialEndDate = now;
                break;
            case "Last Month":
                var lastMonth = now.AddMonths(-1);
                FinancialStartDate = new DateTime(lastMonth.Year, lastMonth.Month, 1);
                FinancialEndDate = new DateTime(now.Year, now.Month, 1).AddDays(-1);
                break;
            case "This Quarter":
                var quarterStart = new DateTime(now.Year, ((now.Month - 1) / 3) * 3 + 1, 1);
                FinancialStartDate = quarterStart;
                FinancialEndDate = now;
                break;
            case "Last Quarter":
                var lastQuarterStart = new DateTime(now.Year, ((now.Month - 1) / 3) * 3 + 1, 1).AddMonths(-3);
                FinancialStartDate = lastQuarterStart;
                FinancialEndDate = new DateTime(now.Year, ((now.Month - 1) / 3) * 3 + 1, 1).AddDays(-1);
                break;
            case "This Year":
                FinancialStartDate = new DateTime(now.Year, 1, 1);
                FinancialEndDate = now;
                break;
            case "Last Year":
                FinancialStartDate = new DateTime(now.Year - 1, 1, 1);
                FinancialEndDate = new DateTime(now.Year - 1, 12, 31);
                break;
            case "Custom":
                // Keep current dates
                break;
        }
    }
}