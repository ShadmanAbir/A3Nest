using System.Windows.Input;

namespace A3Nest.Presentation.Components;

public partial class SearchBar : ContentView
{
    public static readonly BindableProperty SearchTextProperty =
        BindableProperty.Create(
            nameof(SearchText),
            typeof(string),
            typeof(SearchBar),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: OnSearchTextChanged);

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(SearchBar),
            "Search...");

    public static readonly BindableProperty SearchCommandProperty =
        BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(SearchBar),
            null);

    public static readonly BindableProperty ClearCommandProperty =
        BindableProperty.Create(
            nameof(ClearCommand),
            typeof(ICommand),
            typeof(SearchBar),
            null);

    public static readonly BindableProperty ShowSearchButtonProperty =
        BindableProperty.Create(
            nameof(ShowSearchButton),
            typeof(bool),
            typeof(SearchBar),
            true);

    public static readonly BindableProperty SearchDelayProperty =
        BindableProperty.Create(
            nameof(SearchDelay),
            typeof(int),
            typeof(SearchBar),
            500); // 500ms delay for auto-search

    public static readonly BindableProperty EnableAutoSearchProperty =
        BindableProperty.Create(
            nameof(EnableAutoSearch),
            typeof(bool),
            typeof(SearchBar),
            false);

    public static readonly BindableProperty MinSearchLengthProperty =
        BindableProperty.Create(
            nameof(MinSearchLength),
            typeof(int),
            typeof(SearchBar),
            2);

    // Computed property for clear button visibility
    public static readonly BindableProperty HasSearchTextProperty =
        BindableProperty.Create(
            nameof(HasSearchText),
            typeof(bool),
            typeof(SearchBar),
            false);

    private CancellationTokenSource? _searchCancellationTokenSource;

    public string SearchText
    {
        get => (string)GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public ICommand SearchCommand
    {
        get => (ICommand)GetValue(SearchCommandProperty);
        set => SetValue(SearchCommandProperty, value);
    }

    public ICommand ClearCommand
    {
        get => (ICommand)GetValue(ClearCommandProperty);
        set => SetValue(ClearCommandProperty, value);
    }

    public bool ShowSearchButton
    {
        get => (bool)GetValue(ShowSearchButtonProperty);
        set => SetValue(ShowSearchButtonProperty, value);
    }

    public int SearchDelay
    {
        get => (int)GetValue(SearchDelayProperty);
        set => SetValue(SearchDelayProperty, value);
    }

    public bool EnableAutoSearch
    {
        get => (bool)GetValue(EnableAutoSearchProperty);
        set => SetValue(EnableAutoSearchProperty, value);
    }

    public int MinSearchLength
    {
        get => (int)GetValue(MinSearchLengthProperty);
        set => SetValue(MinSearchLengthProperty, value);
    }

    public bool HasSearchText
    {
        get => (bool)GetValue(HasSearchTextProperty);
        private set => SetValue(HasSearchTextProperty, value);
    }

    public SearchBar()
    {
        InitializeComponent();
        
        // Set up default clear command
        ClearCommand = new Command(OnClearCommand);
    }

    private static void OnSearchTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SearchBar searchBar)
        {
            var searchText = newValue?.ToString() ?? string.Empty;
            searchBar.HasSearchText = !string.IsNullOrEmpty(searchText);
            
            if (searchBar.EnableAutoSearch)
            {
                searchBar.ScheduleAutoSearch(searchText);
            }
        }
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        HasSearchText = !string.IsNullOrEmpty(e.NewTextValue);
        
        if (EnableAutoSearch)
        {
            ScheduleAutoSearch(e.NewTextValue);
        }
    }

    private void ScheduleAutoSearch(string searchText)
    {
        // Cancel previous search
        _searchCancellationTokenSource?.Cancel();
        _searchCancellationTokenSource = new CancellationTokenSource();

        // Schedule new search with delay
        Task.Run(async () =>
        {
            try
            {
                await Task.Delay(SearchDelay, _searchCancellationTokenSource.Token);
                
                // Check if search text meets minimum length requirement
                if (!string.IsNullOrEmpty(searchText) && searchText.Length >= MinSearchLength)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (SearchCommand?.CanExecute(searchText) == true)
                        {
                            SearchCommand.Execute(searchText);
                        }
                    });
                }
                else if (string.IsNullOrEmpty(searchText))
                {
                    // Clear search results when text is empty
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (SearchCommand?.CanExecute(string.Empty) == true)
                        {
                            SearchCommand.Execute(string.Empty);
                        }
                    });
                }
            }
            catch (TaskCanceledException)
            {
                // Search was cancelled, ignore
            }
        });
    }

    private void OnClearCommand()
    {
        SearchText = string.Empty;
        SearchEntry?.Focus();
        
        // Execute search command with empty text to clear results
        if (SearchCommand?.CanExecute(string.Empty) == true)
        {
            SearchCommand.Execute(string.Empty);
        }
    }

    // Public method to focus the search entry
    public new void Focus()
    {
        SearchEntry?.Focus();
    }

    // Public method to trigger search programmatically
    public void TriggerSearch()
    {
        if (SearchCommand?.CanExecute(SearchText) == true)
        {
            SearchCommand.Execute(SearchText);
        }
    }
}