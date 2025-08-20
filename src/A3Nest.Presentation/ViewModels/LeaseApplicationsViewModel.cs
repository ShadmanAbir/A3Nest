using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Domain.Enums;
using A3Nest.Presentation.Services;
using System.Collections.ObjectModel;

namespace A3Nest.Presentation.ViewModels;

public partial class LeaseApplicationsViewModel : BaseViewModel
{
    private readonly ITenantService _tenantService;
    private readonly IPropertyService _propertyService;
    private readonly ISampleDataService _sampleDataService;

    public LeaseApplicationsViewModel(
        ITenantService tenantService,
        IPropertyService propertyService,
        ISampleDataService sampleDataService)
    {
        _tenantService = tenantService;
        _propertyService = propertyService;
        _sampleDataService = sampleDataService;
        Title = "Lease Applications";
        
        LeaseApplications = new ObservableCollection<LeaseApplicationDto>();
        FilteredApplications = new ObservableCollection<LeaseApplicationDto>();
    }

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private LeaseApplicationDto? selectedApplication;

    [ObservableProperty]
    private bool isSearching;

    [ObservableProperty]
    private string selectedStatus = "All";

    [ObservableProperty]
    private string sortBy = "ApplicationDate";

    [ObservableProperty]
    private bool sortAscending = false; // Most recent first by default

    [ObservableProperty]
    private DateTime? filterStartDate;

    [ObservableProperty]
    private DateTime? filterEndDate;

    [ObservableProperty]
    private int pendingCount;

    [ObservableProperty]
    private int approvedCount;

    [ObservableProperty]
    private int rejectedCount;

    public ObservableCollection<LeaseApplicationDto> LeaseApplications { get; }
    public ObservableCollection<LeaseApplicationDto> FilteredApplications { get; }

    public List<string> StatusOptions { get; } = new()
    {
        "All",
        "Pending",
        "Active",
        "Expired",
        "Terminated"
    };

    public List<string> SortOptions { get; } = new()
    {
        "ApplicationDate",
        "TenantName",
        "PropertyName",
        "ProposedRent",
        "Status"
    };

    public override async Task LoadAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            ClearError();

            LeaseApplications.Clear();
            FilteredApplications.Clear();

            // Load sample lease applications data
            var applications = await _sampleDataService.GetSampleLeaseApplicationsAsync();
            foreach (var application in applications)
            {
                LeaseApplications.Add(application);
            }
            
            UpdateStatusCounts();
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Failed to load lease applications: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        if (IsSearching) return;

        try
        {
            IsSearching = true;
            ClearError();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                ApplyFilters();
                return;
            }

            // Simulate search delay
            await Task.Delay(100);
            
            // Apply search filter through existing filter mechanism
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Search failed: {ex.Message}");
        }
        finally
        {
            IsSearching = false;
        }
    }

    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = string.Empty;
        ApplyFilters();
    }

    [RelayCommand]
    private void ClearDateFilter()
    {
        FilterStartDate = null;
        FilterEndDate = null;
        ApplyFilters();
    }

    [RelayCommand]
    private async Task ViewApplicationDetailsAsync(LeaseApplicationDto? application)
    {
        if (application == null) return;
        
        SelectedApplication = application;
        // Navigation to application details page placeholder
        await Shell.Current.GoToAsync($"//leaseapplications/details?id={application.Id}");
    }

    [RelayCommand]
    private async Task ApproveApplicationAsync(LeaseApplicationDto? application)
    {
        if (application == null || application.Status != LeaseStatus.Pending) return;

        try
        {
            // Confirmation dialog would be shown here
            bool confirmed = await Shell.Current.DisplayAlert(
                "Approve Application", 
                $"Are you sure you want to approve the lease application for {application.Tenant?.FullName}?", 
                "Yes", "No");

            if (!confirmed) return;

            // Placeholder implementation - would call actual service
            // await _leaseApplicationService.ApproveApplicationAsync(application.Id);
            
            application.Status = LeaseStatus.Active;
            application.ApprovalDate = DateTime.UtcNow;
            
            UpdateStatusCounts();
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Failed to approve application: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task RejectApplicationAsync(LeaseApplicationDto? application)
    {
        if (application == null || application.Status != LeaseStatus.Pending) return;

        try
        {
            // In a real implementation, would show a dialog to get rejection reason
            string reason = await Shell.Current.DisplayPromptAsync(
                "Reject Application", 
                "Please provide a reason for rejection:", 
                "OK", "Cancel", 
                "Reason for rejection");

            if (string.IsNullOrWhiteSpace(reason)) return;

            // Placeholder implementation - would call actual service
            // await _leaseApplicationService.RejectApplicationAsync(application.Id, reason);
            
            application.Status = LeaseStatus.Terminated;
            application.RejectionDate = DateTime.UtcNow;
            application.RejectionReason = reason;
            
            UpdateStatusCounts();
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Failed to reject application: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task ViewTenantDetailsAsync(LeaseApplicationDto? application)
    {
        if (application?.Tenant == null) return;
        
        // Navigation to tenant details page placeholder
        await Shell.Current.GoToAsync($"//tenants/details?id={application.TenantId}");
    }

    [RelayCommand]
    private async Task ViewPropertyDetailsAsync(LeaseApplicationDto? application)
    {
        if (application?.Property == null) return;
        
        // Navigation to property details page placeholder
        await Shell.Current.GoToAsync($"//properties/details?id={application.PropertyId}");
    }

    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            ApplyFilters();
        }
    }

    partial void OnSelectedStatusChanged(string value)
    {
        ApplyFilters();
    }

    partial void OnSortByChanged(string value)
    {
        ApplyFilters();
    }

    partial void OnSortAscendingChanged(bool value)
    {
        ApplyFilters();
    }

    partial void OnFilterStartDateChanged(DateTime? value)
    {
        ApplyFilters();
    }

    partial void OnFilterEndDateChanged(DateTime? value)
    {
        ApplyFilters();
    }

    private void UpdateStatusCounts()
    {
        PendingCount = LeaseApplications.Count(a => a.Status == LeaseStatus.Pending);
        ApprovedCount = LeaseApplications.Count(a => a.Status == LeaseStatus.Active);
        RejectedCount = LeaseApplications.Count(a => a.Status == LeaseStatus.Terminated);
    }

    private void ApplyFilters()
    {
        FilteredApplications.Clear();

        var filtered = LeaseApplications.AsEnumerable();

        // Apply status filter
        if (SelectedStatus != "All")
        {
            if (Enum.TryParse<LeaseStatus>(SelectedStatus, out var status))
            {
                filtered = filtered.Where(a => a.Status == status);
            }
        }

        // Apply date range filter
        if (FilterStartDate.HasValue)
        {
            filtered = filtered.Where(a => a.ApplicationDate >= FilterStartDate.Value);
        }
        if (FilterEndDate.HasValue)
        {
            filtered = filtered.Where(a => a.ApplicationDate <= FilterEndDate.Value);
        }

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(a => 
                (a.Tenant?.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (a.Property?.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (a.Unit?.UnitNumber.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        // Apply sorting
        filtered = SortBy switch
        {
            "ApplicationDate" => SortAscending ? filtered.OrderBy(a => a.ApplicationDate) : filtered.OrderByDescending(a => a.ApplicationDate),
            "TenantName" => SortAscending ? filtered.OrderBy(a => a.Tenant?.FullName) : filtered.OrderByDescending(a => a.Tenant?.FullName),
            "PropertyName" => SortAscending ? filtered.OrderBy(a => a.Property?.Name) : filtered.OrderByDescending(a => a.Property?.Name),
            "ProposedRent" => SortAscending ? filtered.OrderBy(a => a.ProposedRent.Amount) : filtered.OrderByDescending(a => a.ProposedRent.Amount),
            "Status" => SortAscending ? filtered.OrderBy(a => a.Status) : filtered.OrderByDescending(a => a.Status),
            _ => filtered.OrderByDescending(a => a.ApplicationDate)
        };

        foreach (var application in filtered)
        {
            FilteredApplications.Add(application);
        }
    }
}