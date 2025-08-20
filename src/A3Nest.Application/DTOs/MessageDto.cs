using A3Nest.Domain.Enums;

namespace A3Nest.Application.DTOs;

public class MessageDto
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public MessageType MessageType { get; set; } = MessageType.User;
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public int? ParentMessageId { get; set; }
    public List<string> Attachments { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation DTOs
    public UserDto? Sender { get; set; }
    public UserDto? Receiver { get; set; }
    public MessageDto? ParentMessage { get; set; }
    public List<MessageDto> Replies { get; set; } = new();
    
    // Computed properties for UI binding
    public string SenderName => Sender?.FullName ?? "Unknown";
}