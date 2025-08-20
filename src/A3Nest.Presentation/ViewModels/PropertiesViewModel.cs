using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Properties;
using A3Nest.Application.Queries.Properties;
using A3Nest.Presentation.Services;
using System.Collections.ObjectModel;

namespace A3Nest.Presentation.ViewModels;

public partial class PropertiesViewModel : BaseViewModel
{
    private readonly IPropertyService _propertyService;
    private readonly ISampleDataService _sampleDataService;

    public PropertiesViewModel(IPropertyService propertyService, ISampleDataService sampleDataService)
    {
        _propertyService = propertyService;
        _sampleDataService = sampleDataService;
        Title = "Properties";
        
        Properties = new ObservableCollection<PropertyDto>();
        FilteredProperties = new ObservableCollection<PropertyDto>();
    }

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private PropertyDto? selectedProperty;

    [ObservableProperty]
    private bool isSearching;

    [ObservableProperty]
    private string selectedPropertyType = "All";

    [ObservableProperty]
    private string sortBy = "Name";

    [ObservableProperty]
    private bool sortAscending = true;

    public ObservableCollection<PropertyDto> Properties { get; }
    public ObservableCollection<PropertyDto> FilteredProperties { get; }

    public List<string> PropertyTypes { get; } = new()
    {
        "All",
        "Residential",
        "Commercial",
        "Mixed"
    };

    public List<string> SortOptions { get; } = new()
    {
        "Name",
        "PropertyType",
        "TotalUnits",
        "PurchaseDate",
        "CurrentValue"
    };

    public override async Task LoadAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            ClearError();

            Properties.Clear();
            FilteredProperties.Clear();

            // Load sample properties data
            var properties = await _sampleDataService.GetSamplePropertiesAsync();
            foreach (var property in properties)
            {
                Properties.Add(property);
            }
            
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Failed to load properties: {ex.Message}");
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
    private async Task AddPropertyAsync()
    {
        // Navigation to add property page placeholder
        await Shell.Current.GoToAsync("//properties/add");
    }

    [RelayCommand]
    private async Task EditPropertyAsync(PropertyDto? property)
    {
        if (property == null) return;
        
        // Navigation to edit property page placeholder
        await Shell.Current.GoToAsync($"//properties/edit?id={property.Id}");
    }

    [RelayCommand]
    private async Task DeletePropertyAsync(PropertyDto? property)
    {
        if (property == null) return;

        try
        {
            // Confirmation dialog would be shown here
            bool confirmed = await Shell.Current.DisplayAlert(
                "Delete Property", 
                $"Are you sure you want to delete {property.Name}?", 
                "Yes", "No");

            if (!confirmed) return;

            // Placeholder implementation - would call actual service
            // await _propertyService.DeletePropertyAsync(property.Id);
            
            Properties.Remove(property);
            FilteredProperties.Remove(property);
        }
        catch (Exception ex)
        {
            SetError($"Failed to delete property: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task ViewPropertyDetailsAsync(PropertyDto? property)
    {
        if (property == null) return;
        
        SelectedProperty = property;
        // Navigation to property details page placeholder
        await Shell.Current.GoToAsync($"//properties/details?id={property.Id}");
    }

    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            ApplyFilters();
        }
    }

    partial void OnSelectedPropertyTypeChanged(string value)
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

    private void ApplyFilters()
    {
        FilteredProperties.Clear();

        var filtered = Properties.AsEnumerable();

        // Apply property type filter
        if (SelectedPropertyType != "All")
        {
            filtered = filtered.Where(p => p.PropertyType.ToString() == SelectedPropertyType);
        }

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(p => 
                p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.Address.Street.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        // Apply sorting
        filtered = SortBy switch
        {
            "Name" => SortAscending ? filtered.OrderBy(p => p.Name) : filtered.OrderByDescending(p => p.Name),
            "PropertyType" => SortAscending ? filtered.OrderBy(p => p.PropertyType) : filtered.OrderByDescending(p => p.PropertyType),
            "TotalUnits" => SortAscending ? filtered.OrderBy(p => p.TotalUnits) : filtered.OrderByDescending(p => p.TotalUnits),
            "PurchaseDate" => SortAscending ? filtered.OrderBy(p => p.PurchaseDate) : filtered.OrderByDescending(p => p.PurchaseDate),
            "CurrentValue" => SortAscending ? filtered.OrderBy(p => p.CurrentValue.Amount) : filtered.OrderByDescending(p => p.CurrentValue.Amount),
            _ => filtered.OrderBy(p => p.Name)
        };

        foreach (var property in filtered)
        {
            FilteredProperties.Add(property);
        }
    }
}