using A3Nest.Application.DTOs;
using A3Nest.Application.Interfaces;
using A3Nest.Domain.Entities;
using Nest;

namespace A3Nest.Infrastructure.Search;

public class ElasticsearchIndexer : IElasticsearchIndexer
{
    private readonly IElasticClient _elasticClient;

    public ElasticsearchIndexer(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async System.Threading.Tasks.Task IndexPropertyAsync(Property property)
    {
        throw new NotImplementedException("IndexPropertyAsync method not yet implemented");
    }

    public async System.Threading.Tasks.Task IndexTenantAsync(Tenant tenant)
    {
        throw new NotImplementedException("IndexTenantAsync method not yet implemented");
    }

    public async System.Threading.Tasks.Task<IEnumerable<PropertyDto>> SearchPropertiesAsync(string query)
    {
        throw new NotImplementedException("SearchPropertiesAsync method not yet implemented");
    }

    public async System.Threading.Tasks.Task<IEnumerable<TenantDto>> SearchTenantsAsync(string query)
    {
        throw new NotImplementedException("SearchTenantsAsync method not yet implemented");
    }
}