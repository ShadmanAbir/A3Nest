using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Calendar;
using A3Nest.Application.Queries.Calendar;
using A3Nest.Application.Interfaces;

namespace A3Nest.Application.Services;

public class CalendarService : ICalendarService
{
    public Task<CalendarEventDto> GetEventAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CalendarEventDto>> GetEventsAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CalendarEventDto>> GetEventsByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CalendarEventDto>> GetUpcomingEventsAsync(int userId, int days = 7)
    {
        throw new NotImplementedException();
    }

    public Task<CalendarEventDto> CreateEventAsync(CreateCalendarEventCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<CalendarEventDto> UpdateEventAsync(UpdateCalendarEventCommand command)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEventAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CalendarEventDto> AddAttendeeAsync(int eventId, int attendeeId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAttendeeAsync(int eventId, int attendeeId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CalendarEventDto>> SearchEventsAsync(SearchCalendarEventsQuery query)
    {
        throw new NotImplementedException();
    }
}