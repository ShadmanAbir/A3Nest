
namespace A3Nest.Presentation.Services;

public interface IThemeService
{
    /// <summary>
    /// Gets the current application theme
    /// </summary>
    AppTheme CurrentTheme { get; }
    
    /// <summary>
    /// Gets whether the app is using system theme
    /// </summary>
    bool IsSystemTheme { get; }
    
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
    /// Sets the app to follow system theme
    /// </summary>
    void UseSystemTheme();
    
    /// <summary>
    /// Event raised when the theme changes
    /// </summary>
    event EventHandler<AppTheme> ThemeChanged;
    
    /// <summary>
    /// Initializes theme monitoring for system theme changes
    /// </summary>
    void Initialize();
}