namespace A3Nest.Presentation.Components;

public partial class LoadingIndicator : ContentView
{
    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(LoadingIndicator),
            false);

    public static readonly BindableProperty LoadingTextProperty =
        BindableProperty.Create(
            nameof(LoadingText),
            typeof(string),
            typeof(LoadingIndicator),
            "Loading...");

    public static readonly BindableProperty ProgressProperty =
        BindableProperty.Create(
            nameof(Progress),
            typeof(double),
            typeof(LoadingIndicator),
            0.0);

    public static readonly BindableProperty ShowProgressProperty =
        BindableProperty.Create(
            nameof(ShowProgress),
            typeof(bool),
            typeof(LoadingIndicator),
            false);

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public string LoadingText
    {
        get => (string)GetValue(LoadingTextProperty);
        set => SetValue(LoadingTextProperty, value);
    }

    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    public bool ShowProgress
    {
        get => (bool)GetValue(ShowProgressProperty);
        set => SetValue(ShowProgressProperty, value);
    }

    public LoadingIndicator()
    {
        InitializeComponent();
    }
}