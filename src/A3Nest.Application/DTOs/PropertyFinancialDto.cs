namespace A3Nest.Application.DTOs;

public class PropertyFinancialDto
{
    public int PropertyId { get; set; }
    public string PropertyName { get; set; } = string.Empty;
    public MoneyDto Income { get; set; } = new();
    public MoneyDto Expenses { get; set; } = new();
    public MoneyDto NetIncome { get; set; } = new();
    public decimal OccupancyRate { get; set; }
}