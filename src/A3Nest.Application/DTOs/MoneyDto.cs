namespace A3Nest.Application.DTOs;

public class MoneyDto
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    
    public override string ToString()
    {
        return $"{Amount:C} {Currency}";
    }
}