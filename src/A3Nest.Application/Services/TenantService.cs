using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Tenants;
using A3Nest.Application.Queries.Tenants;
using A3Nest.Application.Interfaces;

namespace A3Nest.Application.Services;

public class TenantService : ITenantService
{
    public Task<TenantDto> GetTenantAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TenantDto>> GetTenantsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TenantDto>> GetActiveTenantsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TenantDto> CreateTenantAsync(CreateTenantCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<TenantDto> UpdateTenantAsync(UpdateTenantCommand command)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTenantAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TenantDto>> SearchTenantsAsync(SearchTenantsQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<TenantDto> DeactivateTenantAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TenantDto> ActivateTenantAsync(int id)
    {
        throw new NotImplementedException();
    }
}