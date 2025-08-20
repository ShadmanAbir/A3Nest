using A3Nest.Presentation.Services;
using System.Windows.Input;

namespace A3Nest.Presentation.Components;

public partial class ThemeToggle : ContentView
{
    private IThemeService? _themeService;

    public static readonly BindableProperty CurrentThemeTextProperty =
        BindableProperty.Create(
            nameof(CurrentThemeText),
            typeof(string),
            typeof(ThemeToggle),
            "System");

    public string CurrentThemeText
    {
        get => (string)GetValue(CurrentThemeTextProperty);
        private set => SetValue(CurrentThemeTextProperty, value);
    }

    public ICommand SetLightThemeCommand { get; private set; } = null!;
    public ICommand SetDarkThemeCommand { get; private set; } = null!;
    public ICommand SetSystemThemeCommand { get; private set; } = null!;
    public ICommand ToggleThemeCommand { get; private set; } = null!;

    public ThemeToggle()
    {
        InitializeComponent();
        
        // Initialize commands with placeholder actions
        InitializeCommands();
    }

    private void InitializeCommands()
    {
        SetLightThemeCommand = new Command(() => _themeService?.SetTheme(AppTheme.Light));
        SetDarkThemeCommand = new Command(() => _themeService?.SetTheme(AppTheme.Dark));
        SetSystemThemeCommand = new Command(() => _themeService?.UseSystemTheme());
        ToggleThemeCommand = new Command(() => _themeService?.ToggleTheme());
    }

    private void OnThemeChanged(object? sender, AppTheme theme)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            UpdateCurrentTheme();
            UpdateThemeOptionStyles();
        });
    }

    private void UpdateCurrentTheme()
    {
        if (_themeService != null)
        {
            CurrentThemeText = _themeService.IsSystemTheme ? "System" : 
                              _themeService.CurrentTheme == AppTheme.Light ? "Light" : "Dark";
        }
    }

    private void UpdateThemeOptionStyles()
    {
        if (_themeService == null) return;
        
        // Reset all options to normal state
        VisualStateManager.GoToState(LightThemeOption, "Normal");
        VisualStateManager.GoToState(DarkThemeOption, "Normal");
        VisualStateManager.GoToState(SystemThemeOption, "Normal");

        // Set selected state for current theme
        if (_themeService.IsSystemTheme)
        {
            VisualStateManager.GoToState(SystemThemeOption, "Selected");
        }
        else if (_themeService.CurrentTheme == AppTheme.Light)
        {
            VisualStateManager.GoToState(LightThemeOption, "Selected");
        }
        else
        {
            VisualStateManager.GoToState(DarkThemeOption, "Selected");
        }
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        if (Handler?.MauiContext?.Services != null && _themeService == null)
        {
            try
            {
                var themeService = Handler.MauiContext.Services.GetService<IThemeService>();
                if (themeService != null)
                {
                    _themeService = themeService;
                    
                    // Re-initialize commands with actual service
                    InitializeCommands();
                    
                    UpdateCurrentTheme();
                    UpdateThemeOptionStyles();
                    
                    // Subscribe to theme changes
                    _themeService.ThemeChanged += OnThemeChanged;
                }
            }
            catch
            {
                // Service not available yet, will be updated when theme changes
            }
        }
    }
}