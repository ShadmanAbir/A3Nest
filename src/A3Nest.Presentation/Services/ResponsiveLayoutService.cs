using Microsoft.Extensions.Logging;

namespace A3Nest.Presentation.Services;

public class ResponsiveLayoutService : IResponsiveLayoutService
{
    private const double MobileBreakpoint = 768;
    private const double TabletBreakpoint = 1024;
    private const double DesktopBreakpoint = 1200;
    
    private DeviceType _currentDeviceType;
    private ScreenOrientation _currentOrientation;
    private double _screenWidth;
    private double _screenHeight;
    private readonly ILogger<ResponsiveLayoutService> _logger;
    
    public DeviceType CurrentDeviceType => _currentDeviceType;
    public ScreenOrientation CurrentOrientation => _currentOrientation;
    public double ScreenWidth => _screenWidth;
    public double ScreenHeight => _screenHeight;
    
    public bool ShouldUseMobileNavigation => _currentDeviceType == DeviceType.Mobile;
    public bool ShouldUseDesktopSidebar => _currentDeviceType == DeviceType.Desktop;
    
    public event EventHandler<DeviceType>? LayoutChanged;
    
    public ResponsiveLayoutService(ILogger<ResponsiveLayoutService> logger)
    {
        _logger = logger;
        UpdateScreenInfo();
    }
    
    public void Initialize()
    {
        try
        {
            UpdateScreenInfo();
            
            // Monitor for screen changes using DeviceDisplay
            DeviceDisplay.MainDisplayInfoChanged += OnDisplayInfoChanged;
            
            _logger.LogInformation("ResponsiveLayoutService initialized. Device: {DeviceType}, Screen: {Width}x{Height}", 
                _currentDeviceType, _screenWidth, _screenHeight);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize ResponsiveLayoutService");
        }
    }
    
    public int GetGridColumnCount()
    {
        return _currentDeviceType switch
        {
            DeviceType.Mobile => _currentOrientation == ScreenOrientation.Portrait ? 1 : 2,
            DeviceType.Tablet => _currentOrientation == ScreenOrientation.Portrait ? 2 : 3,
            DeviceType.Desktop => _currentOrientation == ScreenOrientation.Portrait ? 3 : 4,
            _ => 1
        };
    }
    
    public double GetSpacing()
    {
        return _currentDeviceType switch
        {
            DeviceType.Mobile => 8,
            DeviceType.Tablet => 16,
            DeviceType.Desktop => 24,
            _ => 16
        };
    }
    
    public Thickness GetPadding()
    {
        return _currentDeviceType switch
        {
            DeviceType.Mobile => new Thickness(12),
            DeviceType.Tablet => new Thickness(20),
            DeviceType.Desktop => new Thickness(32),
            _ => new Thickness(16)
        };
    }
    
    public double GetFontSize(string sizeCategory)
    {
        var baseSize = sizeCategory.ToLower() switch
        {
            "small" => 12,
            "medium" => 14,
            "large" => 16,
            "xlarge" => 18,
            "heading" => 24,
            _ => 14
        };
        
        return _currentDeviceType switch
        {
            DeviceType.Mobile => baseSize * 0.9,
            DeviceType.Tablet => baseSize,
            DeviceType.Desktop => baseSize * 1.1,
            _ => baseSize
        };
    }
    
    public double GetIconSize()
    {
        return _currentDeviceType switch
        {
            DeviceType.Mobile => 20,
            DeviceType.Tablet => 24,
            DeviceType.Desktop => 28,
            _ => 24
        };
    }
    
    public double GetButtonHeight()
    {
        return _currentDeviceType switch
        {
            DeviceType.Mobile => 44,
            DeviceType.Tablet => 48,
            DeviceType.Desktop => 52,
            _ => 48
        };
    }
    
    public GridLength GetSidebarWidth()
    {
        return _currentDeviceType switch
        {
            DeviceType.Desktop => new GridLength(280),
            DeviceType.Tablet => new GridLength(240),
            _ => new GridLength(0)
        };
    }
    
    public bool ShouldUseCompactLayout()
    {
        return _currentDeviceType == DeviceType.Mobile || 
               (_currentDeviceType == DeviceType.Tablet && _currentOrientation == ScreenOrientation.Portrait);
    }
    
    private void OnDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        try
        {
            UpdateScreenInfo();
            _logger.LogDebug("Display info changed. New device type: {DeviceType}", _currentDeviceType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling display info change");
        }
    }
    
    private void UpdateScreenInfo()
    {
        try
        {
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
            
            _screenWidth = displayInfo.Width / displayInfo.Density;
            _screenHeight = displayInfo.Height / displayInfo.Density;
            
            var previousDeviceType = _currentDeviceType;
            var previousOrientation = _currentOrientation;
            
            _currentDeviceType = _screenWidth switch
            {
                < MobileBreakpoint => DeviceType.Mobile,
                < TabletBreakpoint => DeviceType.Tablet,
                _ => DeviceType.Desktop
            };
            
            _currentOrientation = _screenWidth > _screenHeight 
                ? ScreenOrientation.Landscape 
                : ScreenOrientation.Portrait;
            
            if (previousDeviceType != _currentDeviceType || previousOrientation != _currentOrientation)
            {
                _logger.LogInformation("Layout changed: {DeviceType} {Orientation} ({Width}x{Height})", 
                    _currentDeviceType, _currentOrientation, _screenWidth, _screenHeight);
                LayoutChanged?.Invoke(this, _currentDeviceType);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating screen info");
        }
    }
}