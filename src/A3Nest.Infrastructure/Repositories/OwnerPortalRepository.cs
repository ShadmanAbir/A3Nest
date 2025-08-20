using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Application.Queries.OwnerPortal;

namespace A3Nest.Infrastructure.Repositories;

public class OwnerPortalRepository : IOwnerPortalService
{
    private readonly IUnitOfWork _unitOfWork;

    public OwnerPortalRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PropertyDto>> GetOwnerPropertiesAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TenantDto>> GetOwnerTenantsAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<LeaseApplicationDto>> GetOwnerLeaseApplicationsAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ReportDto>> GetOwnerReportsAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public async Task<OwnerDashboardDto> GetOwnerDashboardAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public async Task<OwnerFinancialSummaryDto> GetOwnerFinancialSummaryAsync(int ownerId, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TaskDto>> GetOwnerTasksAsync(int ownerId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MessageDto>> GetOwnerMessagesAsync(int ownerId)
    {
        throw new NotImplementedException();
    }
}