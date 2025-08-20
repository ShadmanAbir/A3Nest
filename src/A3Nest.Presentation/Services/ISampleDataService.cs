using A3Nest.Application.DTOs;

namespace A3Nest.Presentation.Services;

public interface ISampleDataService
{
    /// <summary>
    /// Gets sample property data for demonstration
    /// </summary>
    Task<IEnumerable<PropertyDto>> GetSamplePropertiesAsync();

    /// <summary>
    /// Gets sample tenant data for demonstration
    /// </summary>
    Task<IEnumerable<TenantDto>> GetSampleTenantsAsync();

    /// <summary>
    /// Gets sample user data for demonstration
    /// </summary>
    Task<IEnumerable<UserDto>> GetSampleUsersAsync();

    /// <summary>
    /// Gets sample message data for demonstration
    /// </summary>
    Task<IEnumerable<MessageDto>> GetSampleMessagesAsync();

    /// <summary>
    /// Gets sample task data for demonstration
    /// </summary>
    Task<IEnumerable<TaskDto>> GetSampleTasksAsync();

    /// <summary>
    /// Gets sample calendar event data for demonstration
    /// </summary>
    Task<IEnumerable<CalendarEventDto>> GetSampleCalendarEventsAsync();

    /// <summary>
    /// Gets sample report data for demonstration
    /// </summary>
    Task<IEnumerable<ReportDto>> GetSampleReportsAsync();

    /// <summary>
    /// Gets sample owner financial summary for demonstration
    /// </summary>
    Task<OwnerFinancialSummaryDto> GetSampleOwnerFinancialSummaryAsync();
}