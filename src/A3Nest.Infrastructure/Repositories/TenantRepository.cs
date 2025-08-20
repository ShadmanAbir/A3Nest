using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Tenants;
using A3Nest.Application.Queries.Tenants;

namespace A3Nest.Infrastructure.Repositories;

public class TenantRepository : ITenantService
{
    private readonly IUnitOfWork _unitOfWork;

    public TenantRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TenantDto> GetTenantAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TenantDto>> GetTenantsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TenantDto>> GetActiveTenantsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TenantDto> CreateTenantAsync(CreateTenantCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<TenantDto> UpdateTenantAsync(UpdateTenantCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteTenantAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TenantDto>> SearchTenantsAsync(SearchTenantsQuery query)
    {
        throw new NotImplementedException();
    }

    public async Task<TenantDto> DeactivateTenantAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<TenantDto> ActivateTenantAsync(int id)
    {
        throw new NotImplementedException();
    }
}