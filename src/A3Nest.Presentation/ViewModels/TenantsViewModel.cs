using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Tenants;
using A3Nest.Application.Queries.Tenants;
using System.Collections.ObjectModel;

namespace A3Nest.Presentation.ViewModels;

public partial class TenantsViewModel : BaseViewModel
{
    private readonly ITenantService _tenantService;

    public TenantsViewModel(ITenantService tenantService)
    {
        _tenantService = tenantService;
        Title = "Tenants";
        
        Tenants = new ObservableCollection<TenantDto>();
        FilteredTenants = new ObservableCollection<TenantDto>();
    }

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private TenantDto? selectedTenant;

    [ObservableProperty]
    private bool isSearching;

    [ObservableProperty]
    private string selectedStatus = "All";

    [ObservableProperty]
    private string sortBy = "LastName";

    [ObservableProperty]
    private bool sortAscending = true;

    [ObservableProperty]
    private bool showActiveOnly = true;

    public ObservableCollection<TenantDto> Tenants { get; }
    public ObservableCollection<TenantDto> FilteredTenants { get; }

    public List<string> StatusOptions { get; } = new()
    {
        "All",
        "Active",
        "Inactive"
    };

    public List<string> SortOptions { get; } = new()
    {
        "LastName",
        "FirstName",
        "DateOfBirth",
        "MonthlyIncome",
        "CreatedAt"
    };

    public override async Task LoadAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            ClearError();

            // Placeholder implementation - would call actual service
            await Task.Delay(100); // Simulate async operation
            
            Tenants.Clear();
            FilteredTenants.Clear();

            // In real implementation:
            // var tenants = ShowActiveOnly 
            //     ? await _tenantService.GetActiveTenantsAsync()
            //     : await _tenantService.GetTenantsAsync();
            // foreach (var tenant in tenants)
            //     Tenants.Add(tenant);
            
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Failed to load tenants: {ex.Message}");
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

            // Placeholder implementation - would call actual search service
            await Task.Delay(100); // Simulate async operation
            
            // In real implementation:
            // var searchQuery = new SearchTenantsQuery { SearchTerm = SearchText };
            // var searchResults = await _tenantService.SearchTenantsAsync(searchQuery);
            // FilteredTenants.Clear();
            // foreach (var tenant in searchResults)
            //     FilteredTenants.Add(tenant);
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
    private async Task AddTenantAsync()
    {
        // Navigation to add tenant page placeholder
        await Shell.Current.GoToAsync("//tenants/add");
    }

    [RelayCommand]
    private async Task EditTenantAsync(TenantDto? tenant)
    {
        if (tenant == null) return;
        
        // Navigation to edit tenant page placeholder
        await Shell.Current.GoToAsync($"//tenants/edit?id={tenant.Id}");
    }

    [RelayCommand]
    private async Task DeactivateTenantAsync(TenantDto? tenant)
    {
        if (tenant == null || !tenant.IsActive) return;

        try
        {
            // Confirmation dialog would be shown here
            bool confirmed = await Shell.Current.DisplayAlert(
                "Deactivate Tenant", 
                $"Are you sure you want to deactivate {tenant.FullName}?", 
                "Yes", "No");

            if (!confirmed) return;

            // Placeholder implementation - would call actual service
            // await _tenantService.DeactivateTenantAsync(tenant.Id);
            
            tenant.IsActive = false;
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Failed to deactivate tenant: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task ActivateTenantAsync(TenantDto? tenant)
    {
        if (tenant == null || tenant.IsActive) return;

        try
        {
            // Placeholder implementation - would call actual service
            // await _tenantService.ActivateTenantAsync(tenant.Id);
            
            tenant.IsActive = true;
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Failed to activate tenant: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task ViewTenantDetailsAsync(TenantDto? tenant)
    {
        if (tenant == null) return;
        
        SelectedTenant = tenant;
        // Navigation to tenant details page placeholder
        await Shell.Current.GoToAsync($"//tenants/details?id={tenant.Id}");
    }

    [RelayCommand]
    private async Task ViewLeaseApplicationsAsync(TenantDto? tenant)
    {
        if (tenant == null) return;
        
        // Navigation to tenant's lease applications placeholder
        await Shell.Current.GoToAsync($"//leaseapplications?tenantId={tenant.Id}");
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

    partial void OnShowActiveOnlyChanged(bool value)
    {
        _ = LoadAsync(); // Reload data when filter changes
    }

    private void ApplyFilters()
    {
        FilteredTenants.Clear();

        var filtered = Tenants.AsEnumerable();

        // Apply status filter
        if (SelectedStatus != "All")
        {
            bool isActive = SelectedStatus == "Active";
            filtered = filtered.Where(t => t.IsActive == isActive);
        }

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(t => 
                t.FirstName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                t.LastName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                t.ContactInfo.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                t.ContactInfo.PhoneNumber.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        // Apply sorting
        filtered = SortBy switch
        {
            "LastName" => SortAscending ? filtered.OrderBy(t => t.LastName) : filtered.OrderByDescending(t => t.LastName),
            "FirstName" => SortAscending ? filtered.OrderBy(t => t.FirstName) : filtered.OrderByDescending(t => t.FirstName),
            "DateOfBirth" => SortAscending ? filtered.OrderBy(t => t.DateOfBirth) : filtered.OrderByDescending(t => t.DateOfBirth),
            "MonthlyIncome" => SortAscending ? filtered.OrderBy(t => t.MonthlyIncome.Amount) : filtered.OrderByDescending(t => t.MonthlyIncome.Amount),
            "CreatedAt" => SortAscending ? filtered.OrderBy(t => t.CreatedAt) : filtered.OrderByDescending(t => t.CreatedAt),
            _ => filtered.OrderBy(t => t.LastName)
        };

        foreach (var tenant in filtered)
        {
            FilteredTenants.Add(tenant);
        }
    }
}