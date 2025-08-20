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
    
    public DeviceType CurrentDeviceType => _currentDeviceType;
    public ScreenOrientation CurrentOrientation => _currentOrientation;
    public double ScreenWidth => _screenWidth;
    public double ScreenHeight => _screenHeight;
    
    public bool ShouldUseMobileNavigation => _currentDeviceType == DeviceType.Mobile;
    public bool ShouldUseDesktopSidebar => _currentDeviceType == DeviceType.Desktop;
    
    public event EventHandler<DeviceType>? LayoutChanged;
    
    public ResponsiveLayoutService()
    {
        UpdateScreenInfo();
    }
    
    public void Initialize()
    {
        UpdateScreenInfo();
        
        // Monitor for screen changes
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping("ResponsiveLayout", (handler, view) =>
        {
            UpdateScreenInfo();
        });
    }
    
    public int GetGridColumnCount()
    {
        return _currentDeviceType switch
        {
            DeviceType.Mobile => _currentOrientation == ScreenOrientation.Portrait ? 1 : 2,
            DeviceType.Tablet => _currentOrientation == ScreenOrientation.Portrait ? 2 : 3,
            DeviceType.Desktop => 4,
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
            DeviceType.Mobile => new Thickness(8),
            DeviceType.Tablet => new Thickness(16),
            DeviceType.Desktop => new Thickness(32),
            _ => new Thickness(16)
        };
    }
    
    private void UpdateScreenInfo()
    {
        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        
        _screenWidth = displayInfo.Width / displayInfo.Density;
        _screenHeight = displayInfo.Height / displayInfo.Density;
        
        var previousDeviceType = _currentDeviceType;
        
        _currentDeviceType = _screenWidth switch
        {
            < MobileBreakpoint => DeviceType.Mobile,
            < TabletBreakpoint => DeviceType.Tablet,
            _ => DeviceType.Desktop
        };
        
        _currentOrientation = _screenWidth > _screenHeight 
            ? ScreenOrientation.Landscape 
            : ScreenOrientation.Portrait;
        
        if (previousDeviceType != _currentDeviceType)
        {
            LayoutChanged?.Invoke(this, _currentDeviceType);
        }
    }
}