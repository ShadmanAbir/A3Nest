using A3Nest.Application.DTOs;
using A3Nest.Application.Queries.OwnerPortal;
using A3Nest.Application.Interfaces;

namespace A3Nest.Application.Services;

public class OwnerPortalService : IOwnerPortalService
{
    public Task<IEnumerable<PropertyDto>> GetOwnerPropertiesAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TenantDto>> GetOwnerTenantsAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LeaseApplicationDto>> GetOwnerLeaseApplicationsAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReportDto>> GetOwnerReportsAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public Task<OwnerDashboardDto> GetOwnerDashboardAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public Task<OwnerFinancialSummaryDto> GetOwnerFinancialSummaryAsync(int ownerId, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskDto>> GetOwnerTasksAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MessageDto>> GetOwnerMessagesAsync(int ownerId)
    {
        throw new NotImplementedException();
    }
}