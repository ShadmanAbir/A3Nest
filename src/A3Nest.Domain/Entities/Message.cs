using A3Nest.Domain.Common;
using A3Nest.Domain.Enums;

namespace A3Nest.Domain.Entities;

public class Message : BaseEntity
{
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
    
    // Navigation properties
    public virtual User Sender { get; set; } = null!;
    public virtual User Receiver { get; set; } = null!;
    public virtual Message? ParentMessage { get; set; }
    public virtual ICollection<Message> Replies { get; set; } = new List<Message>();
}