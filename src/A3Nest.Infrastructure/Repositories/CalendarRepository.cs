using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Calendar;
using A3Nest.Application.Queries.Calendar;

namespace A3Nest.Infrastructure.Repositories;

public class CalendarRepository : ICalendarService
{
    private readonly IUnitOfWork _unitOfWork;

    public CalendarRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CalendarEventDto> GetEventAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CalendarEventDto>> GetEventsAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CalendarEventDto>> GetEventsByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CalendarEventDto>> GetUpcomingEventsAsync(int userId, int days = 7)
    {
        throw new NotImplementedException();
    }

    public async Task<CalendarEventDto> CreateEventAsync(CreateCalendarEventCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<CalendarEventDto> UpdateEventAsync(UpdateCalendarEventCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteEventAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CalendarEventDto> AddAttendeeAsync(int eventId, int attendeeId)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAttendeeAsync(int eventId, int attendeeId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CalendarEventDto>> SearchEventsAsync(SearchCalendarEventsQuery query)
    {
        throw new NotImplementedException();
    }
}