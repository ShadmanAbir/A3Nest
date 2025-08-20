
using Microsoft.Extensions.Logging;

namespace A3Nest.Presentation.Services;

public class ThemeService : IThemeService
{
    private AppTheme _currentTheme;
    private readonly ILogger<ThemeService> _logger;
    private readonly IPreferences _preferences;
    private const string ThemePreferenceKey = "app_theme";
    
    public AppTheme CurrentTheme => _currentTheme;
    
    public event EventHandler<AppTheme>? ThemeChanged;
    
    public ThemeService(ILogger<ThemeService> logger)
    {
        _logger = logger;
        _preferences = Preferences.Default;
        
        // Load saved theme preference or default to system
        var savedTheme = _preferences.Get(ThemePreferenceKey, (int)AppTheme.Unspecified);
        _currentTheme = (AppTheme)savedTheme;
        
        _logger.LogInformation("ThemeService initialized with theme: {Theme}", _currentTheme);
    }
    
    public void Initialize()
    {
        try
        {
            // Monitor system theme changes
            if (Microsoft.Maui.Controls.Application.Current != null)
            {
                Microsoft.Maui.Controls.Application.Current.RequestedThemeChanged += OnSystemThemeChanged;
                
                // Apply the current theme
                ApplyTheme(_currentTheme);
                
                _logger.LogInformation("Theme service initialized and monitoring system theme changes");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize theme service");
        }
    }
    
    public void SetTheme(AppTheme theme)
    {
        if (_currentTheme == theme)
            return;
            
        _logger.LogInformation("Changing theme from {OldTheme} to {NewTheme}", _currentTheme, theme);
        
        _currentTheme = theme;
        
        // Save theme preference
        _preferences.Set(ThemePreferenceKey, (int)theme);
        
        ApplyTheme(theme);
        
        ThemeChanged?.Invoke(this, theme);
    }
    
    public void ToggleTheme()
    {
        var newTheme = _currentTheme switch
        {
            AppTheme.Light => AppTheme.Dark,
            AppTheme.Dark => AppTheme.Light,
            AppTheme.Unspecified => GetSystemTheme() == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark,
            _ => AppTheme.Light
        };
        
        SetTheme(newTheme);
    }
    
    public void UseSystemTheme()
    {
        SetTheme(AppTheme.Unspecified);
    }
    
    public bool IsSystemTheme => _currentTheme == AppTheme.Unspecified;
    
    private void ApplyTheme(AppTheme theme)
    {
        try
        {
            if (Microsoft.Maui.Controls.Application.Current != null)
            {
                Microsoft.Maui.Controls.Application.Current.UserAppTheme = theme;
                _logger.LogDebug("Applied theme: {Theme}", theme);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to apply theme: {Theme}", theme);
        }
    }
    
    private AppTheme GetSystemTheme()
    {
        return Microsoft.Maui.Controls.Application.Current?.RequestedTheme ?? AppTheme.Light;
    }
    
    private void OnSystemThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        try
        {
            _logger.LogInformation("System theme changed to: {Theme}", e.RequestedTheme);
            
            // Only update if we're following system theme (Unspecified)
            if (_currentTheme == AppTheme.Unspecified)
            {
                ThemeChanged?.Invoke(this, e.RequestedTheme);
                _logger.LogInformation("Updated app theme to match system: {Theme}", e.RequestedTheme);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling system theme change");
        }
    }
}