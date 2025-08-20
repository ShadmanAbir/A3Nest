using A3Nest.Presentation.ViewModels;

namespace A3Nest.Presentation.Pages;

public partial class MessagingPage : ContentPage
{
    private readonly MessagingViewModel _viewModel;

    public MessagingPage(MessagingViewModel viewModel)
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