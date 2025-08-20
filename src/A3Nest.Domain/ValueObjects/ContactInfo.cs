namespace A3Nest.Domain.ValueObjects;

public record ContactInfo(
    string Email,
    string PhoneNumber,
    string? AlternatePhoneNumber = null)
{
    public static ContactInfo Empty => new(string.Empty, string.Empty);
    
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Email) && 
               !string.IsNullOrWhiteSpace(PhoneNumber) &&
               Email.Contains('@');
    }
    
    public override string ToString()
    {
        var result = $"Email: {Email}, Phone: {PhoneNumber}";
        if (!string.IsNullOrWhiteSpace(AlternatePhoneNumber))
            result += $", Alt Phone: {AlternatePhoneNumber}";
        return result;
    }
}