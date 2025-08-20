using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Calendar;
using A3Nest.Application.Queries.Calendar;
using A3Nest.Presentation.Services;
using System.Collections.ObjectModel;

namespace A3Nest.Presentation.ViewModels;

public partial class CalendarViewModel : BaseViewModel
{
    private readonly ICalendarService _calendarService;
    private readonly ISampleDataService _sampleDataService;

    public CalendarViewModel(ICalendarService calendarService, ISampleDataService sampleDataService)
    {
        _calendarService = calendarService;
        _sampleDataService = sampleDataService;
        Title = "Calendar";
        
        Events = new ObservableCollection<CalendarEventDto>();
        FilteredEvents = new ObservableCollection<CalendarEventDto>();
        UpcomingEvents = new ObservableCollection<CalendarEventDto>();
        TodayEvents = new ObservableCollection<CalendarEventDto>();
        
        CurrentDate = DateTime.Today;
        SelectedDate = DateTime.Today;
        ViewStartDate = GetWeekStart(DateTime.Today);
        ViewEndDate = GetWeekEnd(DateTime.Today);
    }

    [ObservableProperty]
    private int currentUserId = 1; // Placeholder - would come from authentication

    [ObservableProperty]
    private DateTime currentDate;

    [ObservableProperty]
    private DateTime selectedDate;

    [ObservableProperty]
    private DateTime viewStartDate;

    [ObservableProperty]
    private DateTime viewEndDate;

    [ObservableProperty]
    private string selectedView = "Week";

    [ObservableProperty]
    private CalendarEventDto? selectedEvent;

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private bool isSearching;

    [ObservableProperty]
    private bool isCreatingEvent;

    [ObservableProperty]
    private string newEventTitle = string.Empty;

    [ObservableProperty]
    private string newEventDescription = string.Empty;

    [ObservableProperty]
    private DateTime newEventStartDate = DateTime.Now;

    [ObservableProperty]
    private DateTime newEventEndDate = DateTime.Now.AddHours(1);

    [ObservableProperty]
    private string newEventLocation = string.Empty;

    [ObservableProperty]
    private bool newEventIsAllDay;

    [ObservableProperty]
    private bool isSavingEvent;

    // Additional properties for calendar events
    [ObservableProperty]
    private DateTime startDate = DateTime.Now;

    [ObservableProperty]
    private string description = "Sample Description";

    [ObservableProperty]
    private string location = "Sample Location";

    public string CurrentMonthYear => CurrentDate.ToString("MMMM yyyy");

    public ObservableCollection<CalendarEventDto> Events { get; }
    public ObservableCollection<CalendarEventDto> FilteredEvents { get; }
    public ObservableCollection<CalendarEventDto> UpcomingEvents { get; }
    public ObservableCollection<CalendarEventDto> TodayEvents { get; }

    public List<string> ViewOptions { get; } = new()
    {
        "Day",
        "Week",
        "Month",
        "Agenda"
    };

    public override async Task LoadAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            ClearError();

            await LoadEventsAsync();
            await LoadUpcomingEventsAsync();
            await LoadTodayEventsAsync();
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Failed to load calendar events: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        if (IsSearching) return;

        try
        {
            IsSearching = true;
            ClearError();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                ApplyFilters();
                return;
            }

            // Simulate search delay
            await Task.Delay(100);
            
            // Apply search filter through existing filter mechanism
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Search failed: {ex.Message}");
        }
        finally
        {
            IsSearching = false;
        }
    }

    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = string.Empty;
        ApplyFilters();
    }

    [RelayCommand]
    private void StartCreatingEvent()
    {
        IsCreatingEvent = true;
        NewEventTitle = string.Empty;
        NewEventDescription = string.Empty;
        NewEventStartDate = SelectedDate.Date.AddHours(DateTime.Now.Hour);
        NewEventEndDate = NewEventStartDate.AddHours(1);
        NewEventLocation = string.Empty;
        NewEventIsAllDay = false;
    }

    [RelayCommand]
    private void CancelCreatingEvent()
    {
        IsCreatingEvent = false;
        ResetNewEventFields();
    }

    [RelayCommand]
    private async Task SaveEventAsync()
    {
        if (IsSavingEvent || string.IsNullOrWhiteSpace(NewEventTitle))
            return;

        try
        {
            IsSavingEvent = true;
            ClearError();

            // Placeholder implementation - would call actual service
            await Task.Delay(100); // Simulate async operation
            
            // In real implementation:
            // var command = new CreateCalendarEventCommand
            // {
            //     Title = NewEventTitle,
            //     Description = NewEventDescription,
            //     StartDate = NewEventStartDate,
            //     EndDate = NewEventEndDate,
            //     Location = NewEventLocation,
            //     IsAllDay = NewEventIsAllDay,
            //     OwnerId = CurrentUserId
            // };
            // var createdEvent = await _calendarService.CreateEventAsync(command);
            // Events.Add(createdEvent);

            CancelCreatingEvent();
            await LoadAsync(); // Refresh events
        }
        catch (Exception ex)
        {
            SetError($"Failed to save event: {ex.Message}");
        }
        finally
        {
            IsSavingEvent = false;
        }
    }

    [RelayCommand]
    private async Task EditEventAsync(CalendarEventDto? eventItem)
    {
        if (eventItem == null) return;
        
        // Navigation to edit event page placeholder
        await Shell.Current.GoToAsync($"//calendar/edit?id={eventItem.Id}");
    }

    [RelayCommand]
    private async Task DeleteEventAsync(CalendarEventDto? eventItem)
    {
        if (eventItem == null) return;

        try
        {
            // Confirmation dialog would be shown here
            bool confirmed = await Shell.Current.DisplayAlert(
                "Delete Event", 
                $"Are you sure you want to delete '{eventItem.Title}'?", 
                "Yes", "No");

            if (!confirmed) return;

            // Placeholder implementation - would call actual service
            // await _calendarService.DeleteEventAsync(eventItem.Id);
            
            Events.Remove(eventItem);
            FilteredEvents.Remove(eventItem);
            UpcomingEvents.Remove(eventItem);
            TodayEvents.Remove(eventItem);
        }
        catch (Exception ex)
        {
            SetError($"Failed to delete event: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task ViewEventDetailsAsync(CalendarEventDto? eventItem)
    {
        if (eventItem == null) return;
        
        SelectedEvent = eventItem;
        
        // Navigation to event details page placeholder
        await Shell.Current.GoToAsync($"//calendar/details?id={eventItem.Id}");
    }

    [RelayCommand]
    private async Task NavigateToPreviousPeriodAsync()
    {
        switch (SelectedView)
        {
            case "Day":
                SelectedDate = SelectedDate.AddDays(-1);
                break;
            case "Week":
                ViewStartDate = ViewStartDate.AddDays(-7);
                ViewEndDate = ViewEndDate.AddDays(-7);
                break;
            case "Month":
                CurrentDate = CurrentDate.AddMonths(-1);
                break;
        }
        
        await LoadAsync();
    }

    [RelayCommand]
    private async Task NavigateToNextPeriodAsync()
    {
        switch (SelectedView)
        {
            case "Day":
                SelectedDate = SelectedDate.AddDays(1);
                break;
            case "Week":
                ViewStartDate = ViewStartDate.AddDays(7);
                ViewEndDate = ViewEndDate.AddDays(7);
                break;
            case "Month":
                CurrentDate = CurrentDate.AddMonths(1);
                break;
        }
        
        await LoadAsync();
    }

    [RelayCommand]
    private async Task NavigateToTodayAsync()
    {
        CurrentDate = DateTime.Today;
        SelectedDate = DateTime.Today;
        ViewStartDate = GetWeekStart(DateTime.Today);
        ViewEndDate = GetWeekEnd(DateTime.Today);
        
        await LoadAsync();
    }

    partial void OnSelectedViewChanged(string value)
    {
        UpdateViewDates();
        _ = LoadAsync();
    }

    partial void OnSelectedDateChanged(DateTime value)
    {
        if (SelectedView == "Day")
        {
            _ = LoadAsync();
        }
    }

    partial void OnCurrentDateChanged(DateTime value)
    {
        if (SelectedView == "Month")
        {
            _ = LoadAsync();
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            ApplyFilters();
        }
    }

    private async Task LoadEventsAsync()
    {
        Events.Clear();
        
        // Load sample calendar events
        var allEvents = await _sampleDataService.GetSampleCalendarEventsAsync();
        
        // Filter events based on current view date range
        var filteredEvents = allEvents.Where(e => 
            e.StartDate >= ViewStartDate && e.StartDate < ViewEndDate);
            
        foreach (var eventItem in filteredEvents)
        {
            Events.Add(eventItem);
        }
    }

    private async Task LoadUpcomingEventsAsync()
    {
        UpcomingEvents.Clear();
        
        // Load sample calendar events and filter for upcoming
        var allEvents = await _sampleDataService.GetSampleCalendarEventsAsync();
        var upcomingEvents = allEvents.Where(e => 
            e.StartDate >= DateTime.Now && e.StartDate <= DateTime.Now.AddDays(7))
            .OrderBy(e => e.StartDate);
            
        foreach (var eventItem in upcomingEvents)
        {
            UpcomingEvents.Add(eventItem);
        }
    }

    private async Task LoadTodayEventsAsync()
    {
        TodayEvents.Clear();
        
        // Load sample calendar events and filter for today
        var allEvents = await _sampleDataService.GetSampleCalendarEventsAsync();
        var todayEvents = allEvents.Where(e => 
            e.StartDate.Date == DateTime.Today)
            .OrderBy(e => e.StartDate);
            
        foreach (var eventItem in todayEvents)
        {
            TodayEvents.Add(eventItem);
        }
    }

    private void UpdateViewDates()
    {
        switch (SelectedView)
        {
            case "Day":
                ViewStartDate = SelectedDate.Date;
                ViewEndDate = SelectedDate.Date.AddDays(1);
                break;
            case "Week":
                ViewStartDate = GetWeekStart(SelectedDate);
                ViewEndDate = GetWeekEnd(SelectedDate);
                break;
            case "Month":
                ViewStartDate = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
                ViewEndDate = ViewStartDate.AddMonths(1);
                break;
            case "Agenda":
                ViewStartDate = DateTime.Today;
                ViewEndDate = DateTime.Today.AddDays(30);
                break;
        }
    }

    private void ApplyFilters()
    {
        FilteredEvents.Clear();

        var filtered = Events.AsEnumerable();

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(e => 
                e.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                e.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                e.Location.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        // Sort by start date
        filtered = filtered.OrderBy(e => e.StartDate);

        foreach (var eventItem in filtered)
        {
            FilteredEvents.Add(eventItem);
        }
    }

    private void ResetNewEventFields()
    {
        NewEventTitle = string.Empty;
        NewEventDescription = string.Empty;
        NewEventStartDate = DateTime.Now;
        NewEventEndDate = DateTime.Now.AddHours(1);
        NewEventLocation = string.Empty;
        NewEventIsAllDay = false;
    }

    private static DateTime GetWeekStart(DateTime date)
    {
        int diff = (7 + (date.DayOfWeek - DayOfWeek.Sunday)) % 7;
        return date.AddDays(-1 * diff).Date;
    }

    private static DateTime GetWeekEnd(DateTime date)
    {
        return GetWeekStart(date).AddDays(7);
    }
}