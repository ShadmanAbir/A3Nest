using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace A3Nest.Infrastructure.Search;

public static class ElasticsearchConfiguration
{
    public static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Elasticsearch") ?? "http://localhost:9200";
        var indexName = configuration["Elasticsearch:IndexName"] ?? "a3nest";

        var settings = new ConnectionSettings(new Uri(connectionString))
            .DefaultIndex(indexName)
            .EnableDebugMode()
            .PrettyJson()
            .RequestTimeout(TimeSpan.FromMinutes(2));

        var client = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(client);

        return services;
    }

    public static async System.Threading.Tasks.Task<bool> CreateIndexIfNotExistsAsync(IElasticClient client, string indexName)
    {
        var existsResponse = await client.Indices.ExistsAsync(indexName);
        if (existsResponse.Exists)
            return true;

        var createIndexResponse = await client.Indices.CreateAsync(indexName, c => c
            .Settings(s => s
                .NumberOfShards(1)
                .NumberOfReplicas(0)
            )
            .Map<PropertySearchDocument>(m => m.AutoMap())
            .Map<TenantSearchDocument>(m => m.AutoMap())
        );

        return createIndexResponse.IsValid;
    }
}