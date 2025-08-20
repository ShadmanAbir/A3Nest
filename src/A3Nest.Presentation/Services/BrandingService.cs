using A3Nest.Presentation.Constants;

namespace A3Nest.Presentation.Services;

/// <summary>
/// Service implementation for managing application branding
/// </summary>
public class BrandingService : IBrandingService
{
    /// <inheritdoc/>
    public string GetAppName()
    {
        return BrandingConstants.AppName;
    }

    /// <inheritdoc/>
    public string GetAppTagline()
    {
        return BrandingConstants.AppTagline;
    }

    /// <inheritdoc/>
    public string GetAppDescription()
    {
        return BrandingConstants.AppDescription;
    }

    /// <inheritdoc/>
    public string GetAppVersion()
    {
        return BrandingConstants.Version;
    }

    /// <inheritdoc/>
    public string GetCopyright()
    {
        return BrandingConstants.Copyright;
    }

    /// <inheritdoc/>
    public string GetPrimaryColor()
    {
        return BrandingConstants.PrimaryColor;
    }

    /// <inheritdoc/>
    public string GetLogoPath()
    {
        return BrandingConstants.LogoFileName;
    }

    /// <inheritdoc/>
    public string GetFormattedAppTitle()
    {
        return $"{BrandingConstants.AppName} - {BrandingConstants.AppTagline}";
    }
}