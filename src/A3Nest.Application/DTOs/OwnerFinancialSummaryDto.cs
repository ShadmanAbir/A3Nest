namespace A3Nest.Application.DTOs;

public class OwnerFinancialSummaryDto
{
    public DateRangeDto Period { get; set; } = new();
    public MoneyDto TotalIncome { get; set; } = new();
    public MoneyDto TotalExpenses { get; set; } = new();
    public MoneyDto NetIncome { get; set; } = new();
    public MoneyDto RentCollected { get; set; } = new();
    public MoneyDto MaintenanceCosts { get; set; } = new();
    public MoneyDto PropertyTaxes { get; set; } = new();
    public MoneyDto Insurance { get; set; } = new();
    public MoneyDto OtherExpenses { get; set; } = new();
    public List<PropertyFinancialDto> PropertyBreakdown { get; set; } = new();
}