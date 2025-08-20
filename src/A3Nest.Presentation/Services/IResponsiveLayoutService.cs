namespace A3Nest.Presentation.Services;

public enum DeviceType
{
    Mobile,
    Tablet,
    Desktop
}

public enum ScreenOrientation
{
    Portrait,
    Landscape
}

public interface IResponsiveLayoutService
{
    /// <summary>
    /// Gets the current device type based on screen size
    /// </summary>
    DeviceType CurrentDeviceType { get; }
    
    /// <summary>
    /// Gets the current screen orientation
    /// </summary>
    ScreenOrientation CurrentOrientation { get; }
    
    /// <summary>
    /// Gets the current screen width
    /// </summary>
    double ScreenWidth { get; }
    
    /// <summary>
    /// Gets the current screen height
    /// </summary>
    double ScreenHeight { get; }
    
    /// <summary>
    /// Determines if the current layout should use mobile navigation
    /// </summary>
    bool ShouldUseMobileNavigation { get; }
    
    /// <summary>
    /// Determines if the current layout should use desktop sidebar
    /// </summary>
    bool ShouldUseDesktopSidebar { get; }
    
    /// <summary>
    /// Gets the appropriate column count for grid layouts
    /// </summary>
    int GetGridColumnCount();
    
    /// <summary>
    /// Gets the appropriate spacing for the current device
    /// </summary>
    double GetSpacing();
    
    /// <summary>
    /// Gets the appropriate padding for the current device
    /// </summary>
    Thickness GetPadding();
    
    /// <summary>
    /// Gets responsive font size for the given category
    /// </summary>
    double GetFontSize(string sizeCategory);
    
    /// <summary>
    /// Gets appropriate icon size for current device
    /// </summary>
    double GetIconSize();
    
    /// <summary>
    /// Gets appropriate button height for current device
    /// </summary>
    double GetButtonHeight();
    
    /// <summary>
    /// Gets sidebar width for current device
    /// </summary>
    GridLength GetSidebarWidth();
    
    /// <summary>
    /// Determines if compact layout should be used
    /// </summary>
    bool ShouldUseCompactLayout();
    
    /// <summary>
    /// Event raised when the layout changes (screen size, orientation)
    /// </summary>
    event EventHandler<DeviceType> LayoutChanged;
    
    /// <summary>
    /// Initializes the service and starts monitoring screen changes
    /// </summary>
    void Initialize();
}