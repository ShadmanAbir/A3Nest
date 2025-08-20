namespace A3Nest.Application.Queries.Tenants;

public class SearchTenantsQuery
{
    public string? SearchTerm { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? IsActive { get; set; }
    public string? EmploymentStatus { get; set; }
    public decimal? MinIncome { get; set; }
    public decimal? MaxIncome { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}