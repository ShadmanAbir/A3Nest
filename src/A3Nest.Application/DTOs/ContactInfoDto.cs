namespace A3Nest.Application.DTOs;

public class ContactInfoDto
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? AlternatePhoneNumber { get; set; }
    public string? AlternateEmail { get; set; }
}