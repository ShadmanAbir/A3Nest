using A3Nest.Domain.Enums;

namespace A3Nest.Application.Queries.Properties;

public class SearchPropertiesQuery
{
    public string? SearchTerm { get; set; }
    public PropertyType? PropertyType { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public int? OwnerId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinUnits { get; set; }
    public int? MaxUnits { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}