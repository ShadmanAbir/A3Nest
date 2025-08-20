using A3Nest.Domain.Enums;
using Nest;

namespace A3Nest.Infrastructure.Search;

[ElasticsearchType(RelationName = "property")]
public class PropertySearchDocument
{
    [Number(Name = "id")]
    public int Id { get; set; }

    [Text(Name = "name", Analyzer = "standard")]
    public string Name { get; set; } = string.Empty;

    [Text(Name = "description", Analyzer = "standard")]
    public string Description { get; set; } = string.Empty;

    [Keyword(Name = "property_type")]
    public PropertyType PropertyType { get; set; }

    [Object(Name = "address")]
    public AddressSearchDocument Address { get; set; } = new();

    [Number(Name = "total_units")]
    public int TotalUnits { get; set; }

    [Number(Name = "purchase_price")]
    public decimal PurchasePrice { get; set; }

    [Number(Name = "current_value")]
    public decimal CurrentValue { get; set; }

    [Date(Name = "purchase_date")]
    public DateTime PurchaseDate { get; set; }

    [Number(Name = "owner_id")]
    public int OwnerId { get; set; }

    [Number(Name = "property_manager_id")]
    public int? PropertyManagerId { get; set; }

    [Date(Name = "created_at")]
    public DateTime CreatedAt { get; set; }

    [Date(Name = "updated_at")]
    public DateTime UpdatedAt { get; set; }
}

public class AddressSearchDocument
{
    [Text(Name = "street", Analyzer = "standard")]
    public string Street { get; set; } = string.Empty;

    [Text(Name = "city", Analyzer = "standard")]
    public string City { get; set; } = string.Empty;

    [Keyword(Name = "state")]
    public string State { get; set; } = string.Empty;

    [Keyword(Name = "postal_code")]
    public string PostalCode { get; set; } = string.Empty;

    [Keyword(Name = "country")]
    public string Country { get; set; } = string.Empty;
}