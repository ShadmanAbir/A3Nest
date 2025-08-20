namespace A3Nest.Presentation.Services;

/// <summary>
/// Service interface for managing application branding
/// </summary>
public interface IBrandingService
{
    /// <summary>
    /// Gets the application name
    /// </summary>
    string GetAppName();

    /// <summary>
    /// Gets the application tagline
    /// </summary>
    string GetAppTagline();

    /// <summary>
    /// Gets the full application description
    /// </summary>
    string GetAppDescription();

    /// <summary>
    /// Gets the application version
    /// </summary>
    string GetAppVersion();

    /// <summary>
    /// Gets the copyright notice
    /// </summary>
    string GetCopyright();

    /// <summary>
    /// Gets the primary brand color
    /// </summary>
    string GetPrimaryColor();

    /// <summary>
    /// Gets the logo file path
    /// </summary>
    string GetLogoPath();

    /// <summary>
    /// Gets the formatted app title with tagline
    /// </summary>
    string GetFormattedAppTitle();
}