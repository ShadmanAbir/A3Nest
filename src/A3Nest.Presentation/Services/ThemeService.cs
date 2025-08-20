

namespace A3Nest.Presentation.Services;

public class ThemeService : IThemeService
{
    private AppTheme _currentTheme;
    
    public AppTheme CurrentTheme => _currentTheme;
    
    public event EventHandler<AppTheme>? ThemeChanged;
    
    public ThemeService()
    {
        _currentTheme = Microsoft.Maui.Controls.Application.Current?.RequestedTheme ?? AppTheme.Unspecified;
    }
    
    public void Initialize()
    {
        // Monitor system theme changes
        if (Microsoft.Maui.Controls.Application.Current != null)
        {
            Microsoft.Maui.Controls.Application.Current.RequestedThemeChanged += OnSystemThemeChanged;
            _currentTheme = Microsoft.Maui.Controls.Application.Current.RequestedTheme;
        }
    }
    
    public void SetTheme(AppTheme theme)
    {
        if (_currentTheme == theme)
            return;
            
        _currentTheme = theme;
        
        if (Microsoft.Maui.Controls.Application.Current != null)
        {
            Microsoft.Maui.Controls.Application.Current.UserAppTheme = theme;
        }
        
        ThemeChanged?.Invoke(this, theme);
    }
    
    public void ToggleTheme()
    {
        var newTheme = _currentTheme switch
        {
            AppTheme.Light => AppTheme.Dark,
            AppTheme.Dark => AppTheme.Light,
            AppTheme.Unspecified => AppTheme.Dark,
            _ => AppTheme.Light
        };
        
        SetTheme(newTheme);
    }
    
    private void OnSystemThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        // Only update if we're following system theme (Unspecified)
        if (Microsoft.Maui.Controls.Application.Current?.UserAppTheme == AppTheme.Unspecified)
        {
            _currentTheme = e.RequestedTheme;
            ThemeChanged?.Invoke(this, _currentTheme);
        }
    }
}