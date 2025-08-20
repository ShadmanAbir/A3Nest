namespace A3Nest.Application.DTOs;

public class DateRangeDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public int DurationInDays => (EndDate - StartDate).Days;
    
    public bool Contains(DateTime date)
    {
        return date >= StartDate && date <= EndDate;
    }
    
    public bool Overlaps(DateRangeDto other)
    {
        return StartDate <= other.EndDate && EndDate >= other.StartDate;
    }
    
    public override string ToString()
    {
        return $"{StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}";
    }
}