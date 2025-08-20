using A3Nest.Presentation.ViewModels;

namespace A3Nest.Presentation.Pages;

public partial class OwnerPortalPage : ContentPage
{
    private readonly OwnerPortalViewModel _viewModel;

    public OwnerPortalPage(OwnerPortalViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        // Load sample data when page appears
        await _viewModel.LoadAsync();
    }
}