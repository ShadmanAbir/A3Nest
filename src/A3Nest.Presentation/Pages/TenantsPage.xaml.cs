using A3Nest.Presentation.ViewModels;

namespace A3Nest.Presentation.Pages;

public partial class TenantsPage : ContentPage
{
    private readonly TenantsViewModel _viewModel;

    public TenantsPage(TenantsViewModel viewModel)
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