using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Presentation.Services;
using System.Collections.ObjectModel;

namespace A3Nest.Presentation.ViewModels;

public partial class GuestViewViewModel : BaseViewModel
{
    private readonly IPropertyService _propertyService;
    private readonly ISampleDataService _sampleDataService;

    public GuestViewViewModel(IPropertyService propertyService, ISampleDataService sampleDataService)
    {
        _propertyService = propertyService;
        _sampleDataService = sampleDataService;
        Title = "Property Search";
        
        AvailableProperties = new ObservableCollection<PropertyDto>();
        FilteredProperties = new ObservableCollection<PropertyDto>();
        FeaturedProperties = new ObservableCollection<PropertyDto>();
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
    private decimal minRent;

    [ObservableProperty]
    private decimal maxRent = 10000;

    [ObservableProperty]
    private int minBedrooms;

    [ObservableProperty]
    private int maxBedrooms = 10;

    [ObservableProperty]
    private int minBathrooms;

    [ObservableProperty]
    private int maxBathrooms = 10;

    [ObservableProperty]
    private string selectedLocation = string.Empty;

    [ObservableProperty]
    private bool petsAllowed;

    [ObservableProperty]
    private bool smokingAllowed;

    [ObservableProperty]
    private bool parkingRequired;

    [ObservableProperty]
    private string sortBy = "Rent";

    [ObservableProperty]
    private bool sortAscending = true;

    [ObservableProperty]
    private bool showAvailableOnly = true;

    [ObservableProperty]
    private string contactName = string.Empty;

    [ObservableProperty]
    private string contactEmail = string.Empty;

    [ObservableProperty]
    private string contactPhone = string.Empty;

    [ObservableProperty]
    private string inquiryMessage = string.Empty;

    [ObservableProperty]
    private bool isSubmittingInquiry;

    public ObservableCollection<PropertyDto> AvailableProperties { get; }
    public ObservableCollection<PropertyDto> FilteredProperties { get; }
    public ObservableCollection<PropertyDto> FeaturedProperties { get; }

    public List<string> PropertyTypeOptions { get; } = new()
    {
        "All",
        "Residential",
        "Commercial",
        "Mixed"
    };

    public List<string> SortOptions { get; } = new()
    {
        "Rent",
        "Name",
        "Location",
        "Bedrooms",
        "Bathrooms",
        "AvailableDate"
    };

    public List<string> LocationOptions { get; } = new()
    {
        "All Locations",
        "Downtown",
        "Suburbs",
        "Waterfront",
        "City Center",
        "Residential Area"
    };

    public override async Task LoadAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            ClearError();

            await LoadAvailablePropertiesAsync();
            await LoadFeaturedPropertiesAsync();
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
    private void ClearFilters()
    {
        SearchText = string.Empty;
        SelectedPropertyType = "All";
        MinRent = 0;
        MaxRent = 10000;
        MinBedrooms = 0;
        MaxBedrooms = 10;
        MinBathrooms = 0;
        MaxBathrooms = 10;
        SelectedLocation = "All Locations";
        PetsAllowed = false;
        SmokingAllowed = false;
        ParkingRequired = false;
        
        ApplyFilters();
    }

    [RelayCommand]
    private async Task ViewPropertyDetailsAsync(PropertyDto? property)
    {
        if (property == null) return;
        
        SelectedProperty = property;
        
        // Navigation to property details page placeholder
        await Shell.Current.GoToAsync($"//guest/property-details?id={property.Id}");
    }

    [RelayCommand]
    private async Task SubmitInquiryAsync(PropertyDto? property)
    {
        if (property == null || IsSubmittingInquiry) return;

        if (string.IsNullOrWhiteSpace(ContactName) || 
            string.IsNullOrWhiteSpace(ContactEmail) || 
            string.IsNullOrWhiteSpace(InquiryMessage))
        {
            SetError("Please fill in all required fields (Name, Email, and Message).");
            return;
        }

        try
        {
            IsSubmittingInquiry = true;
            ClearError();

            // Placeholder implementation - would submit inquiry
            await Task.Delay(500); // Simulate submission
            
            // In real implementation:
            // var inquiry = new PropertyInquiryDto
            // {
            //     PropertyId = property.Id,
            //     ContactName = ContactName,
            //     ContactEmail = ContactEmail,
            //     ContactPhone = ContactPhone,
            //     Message = InquiryMessage,
            //     InquiryDate = DateTime.UtcNow
            // };
            // await _propertyService.SubmitInquiryAsync(inquiry);

            // Clear form
            ContactName = string.Empty;
            ContactEmail = string.Empty;
            ContactPhone = string.Empty;
            InquiryMessage = string.Empty;
            
            await Shell.Current.DisplayAlert(
                "Inquiry Submitted", 
                "Thank you for your interest! The property owner will contact you soon.", 
                "OK");
        }
        catch (Exception ex)
        {
            SetError($"Failed to submit inquiry: {ex.Message}");
        }
        finally
        {
            IsSubmittingInquiry = false;
        }
    }

    [RelayCommand]
    private async Task RequestTourAsync(PropertyDto? property)
    {
        if (property == null) return;

        try
        {
            // Show date/time picker for tour request
            await Shell.Current.DisplayAlert(
                "Request Tour", 
                "Tour request functionality would be implemented here.", 
                "OK");
        }
        catch (Exception ex)
        {
            SetError($"Failed to request tour: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task SaveFavoriteAsync(PropertyDto? property)
    {
        if (property == null) return;

        try
        {
            // Placeholder implementation - would save to favorites
            await Task.Delay(100); // Simulate save operation
            
            await Shell.Current.DisplayAlert(
                "Favorite Saved", 
                $"'{property.Name}' has been added to your favorites.", 
                "OK");
        }
        catch (Exception ex)
        {
            SetError($"Failed to save favorite: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task SharePropertyAsync(PropertyDto? property)
    {
        if (property == null) return;

        try
        {
            // Placeholder implementation - would use platform sharing
            await Shell.Current.DisplayAlert(
                "Share Property", 
                "Property sharing functionality would be implemented here.", 
                "OK");
        }
        catch (Exception ex)
        {
            SetError($"Failed to share property: {ex.Message}");
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            ApplyFilters();
        }
    }

    partial void OnSelectedPropertyTypeChanged(string value) => ApplyFilters();
    partial void OnMinRentChanged(decimal value) => ApplyFilters();
    partial void OnMaxRentChanged(decimal value) => ApplyFilters();
    partial void OnMinBedroomsChanged(int value) => ApplyFilters();
    partial void OnMaxBedroomsChanged(int value) => ApplyFilters();
    partial void OnMinBathroomsChanged(int value) => ApplyFilters();
    partial void OnMaxBathroomsChanged(int value) => ApplyFilters();
    partial void OnSelectedLocationChanged(string value) => ApplyFilters();
    partial void OnPetsAllowedChanged(bool value) => ApplyFilters();
    partial void OnSmokingAllowedChanged(bool value) => ApplyFilters();
    partial void OnParkingRequiredChanged(bool value) => ApplyFilters();
    partial void OnSortByChanged(string value) => ApplyFilters();
    partial void OnSortAscendingChanged(bool value) => ApplyFilters();
    partial void OnShowAvailableOnlyChanged(bool value) => ApplyFilters();

    private async Task LoadAvailablePropertiesAsync()
    {
        AvailableProperties.Clear();
        
        // Load sample properties data
        var properties = await _sampleDataService.GetSamplePropertiesAsync();
        foreach (var property in properties)
        {
            AvailableProperties.Add(property);
        }
    }

    private async Task LoadFeaturedPropertiesAsync()
    {
        FeaturedProperties.Clear();
        
        // Load sample properties data and mark first two as featured
        var properties = await _sampleDataService.GetSamplePropertiesAsync();
        foreach (var property in properties.Take(2))
        {
            FeaturedProperties.Add(property);
        }
    }

    private void ApplyFilters()
    {
        FilteredProperties.Clear();

        var filtered = AvailableProperties.AsEnumerable();

        // Apply property type filter
        if (SelectedPropertyType != "All")
        {
            filtered = filtered.Where(p => p.PropertyType.ToString() == SelectedPropertyType);
        }

        // Apply location filter
        if (!string.IsNullOrEmpty(SelectedLocation) && SelectedLocation != "All Locations")
        {
            filtered = filtered.Where(p => 
                p.Address.City.Contains(SelectedLocation, StringComparison.OrdinalIgnoreCase) ||
                p.Address.State.Contains(SelectedLocation, StringComparison.OrdinalIgnoreCase));
        }

        // Apply rent range filter
        // Note: In real implementation, would filter by actual rent from units
        // filtered = filtered.Where(p => p.Units.Any(u => u.Rent >= MinRent && u.Rent <= MaxRent));

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(p => 
                p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.Address.Street.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.Address.City.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        // Apply sorting
        filtered = SortBy switch
        {
            "Name" => SortAscending ? filtered.OrderBy(p => p.Name) : filtered.OrderByDescending(p => p.Name),
            "Location" => SortAscending ? filtered.OrderBy(p => p.Address.City) : filtered.OrderByDescending(p => p.Address.City),
            "Rent" => SortAscending ? filtered.OrderBy(p => p.CurrentValue.Amount) : filtered.OrderByDescending(p => p.CurrentValue.Amount),
            _ => filtered.OrderBy(p => p.Name)
        };

        foreach (var property in filtered)
        {
            FilteredProperties.Add(property);
        }
    }
}