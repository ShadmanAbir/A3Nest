using A3Nest.Presentation.ViewModels;

namespace A3Nest.Presentation.Pages;

public partial class CalendarPage : ContentPage
{
    private readonly CalendarViewModel _viewModel;

    public CalendarPage(CalendarViewModel viewModel)
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