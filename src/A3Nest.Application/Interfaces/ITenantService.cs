using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Tenants;
using A3Nest.Application.Queries.Tenants;

namespace A3Nest.Application.Interfaces;

public interface ITenantService
{
    Task<TenantDto> GetTenantAsync(int id);
    Task<IEnumerable<TenantDto>> GetTenantsAsync();
    Task<IEnumerable<TenantDto>> GetActiveTenantsAsync();
    Task<TenantDto> CreateTenantAsync(CreateTenantCommand command);
    Task<TenantDto> UpdateTenantAsync(UpdateTenantCommand command);
    Task DeleteTenantAsync(int id);
    Task<IEnumerable<TenantDto>> SearchTenantsAsync(SearchTenantsQuery query);
    Task<TenantDto> DeactivateTenantAsync(int id);
    Task<TenantDto> ActivateTenantAsync(int id);
}