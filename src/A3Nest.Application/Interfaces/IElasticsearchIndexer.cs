using A3Nest.Application.DTOs;
using A3Nest.Domain.Entities;

namespace A3Nest.Application.Interfaces;

public interface IElasticsearchIndexer
{
    System.Threading.Tasks.Task IndexPropertyAsync(Property property);
    System.Threading.Tasks.Task IndexTenantAsync(Tenant tenant);
    System.Threading.Tasks.Task<IEnumerable<PropertyDto>> SearchPropertiesAsync(string query);
    System.Threading.Tasks.Task<IEnumerable<TenantDto>> SearchTenantsAsync(string query);
}