using Microsoft.Maui.Authentication.WebUI;

namespace A3Nest.Presentation.Services;

public interface IThemeService
{
    /// <summary>
    /// Gets the current application theme
    /// </summary>
    AppTheme CurrentTheme { get; }
    
    /// <summary>
    /// Sets the application theme
    /// </summary>
    /// <param name="theme">The theme to set</param>
    void SetTheme(AppTheme theme);
    
    /// <summary>
    /// Toggles between light and dark theme
    /// </summary>
    void ToggleTheme();
    
    /// <summary>
    /// Event raised when the theme changes
    /// </summary>
    event EventHandler<AppTheme> ThemeChanged;
    
    /// <summary>
    /// Initializes theme monitoring for system theme changes
    /// </summary>
    void Initialize();
}