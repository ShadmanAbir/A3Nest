using A3Nest.Application.DTOs;
using A3Nest.Application.Queries.OwnerPortal;

namespace A3Nest.Application.Interfaces;

public interface IOwnerPortalService
{
    Task<IEnumerable<PropertyDto>> GetOwnerPropertiesAsync(int ownerId);
    Task<IEnumerable<TenantDto>> GetOwnerTenantsAsync(int ownerId);
    Task<IEnumerable<LeaseApplicationDto>> GetOwnerLeaseApplicationsAsync(int ownerId);
    Task<IEnumerable<ReportDto>> GetOwnerReportsAsync(int ownerId);
    Task<OwnerDashboardDto> GetOwnerDashboardAsync(int ownerId);
    Task<OwnerFinancialSummaryDto> GetOwnerFinancialSummaryAsync(int ownerId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<TaskDto>> GetOwnerTasksAsync(int ownerId);
    Task<IEnumerable<MessageDto>> GetOwnerMessagesAsync(int ownerId);
}