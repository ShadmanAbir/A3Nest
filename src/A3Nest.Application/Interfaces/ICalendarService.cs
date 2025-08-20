using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Calendar;
using A3Nest.Application.Queries.Calendar;

namespace A3Nest.Application.Interfaces;

public interface ICalendarService
{
    Task<CalendarEventDto> GetEventAsync(int id);
    Task<IEnumerable<CalendarEventDto>> GetEventsAsync(int userId);
    Task<IEnumerable<CalendarEventDto>> GetEventsByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<CalendarEventDto>> GetUpcomingEventsAsync(int userId, int days = 7);
    Task<CalendarEventDto> CreateEventAsync(CreateCalendarEventCommand command);
    Task<CalendarEventDto> UpdateEventAsync(UpdateCalendarEventCommand command);
    Task DeleteEventAsync(int id);
    Task<CalendarEventDto> AddAttendeeAsync(int eventId, int attendeeId);
    Task RemoveAttendeeAsync(int eventId, int attendeeId);
    Task<IEnumerable<CalendarEventDto>> SearchEventsAsync(SearchCalendarEventsQuery query);
}