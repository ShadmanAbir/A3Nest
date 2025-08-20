namespace A3Nest.Domain.ValueObjects;

public record DateRange(DateTime StartDate, DateTime EndDate)
{
    public DateRange() : this(DateTime.MinValue, DateTime.MaxValue) { }
    
    public int DurationInDays => (EndDate - StartDate).Days;
    
    public bool Contains(DateTime date)
    {
        return date >= StartDate && date <= EndDate;
    }
    
    public bool Overlaps(DateRange other)
    {
        return StartDate <= other.EndDate && EndDate >= other.StartDate;
    }
    
    public override string ToString()
    {
        return $"{StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}";
    }
}