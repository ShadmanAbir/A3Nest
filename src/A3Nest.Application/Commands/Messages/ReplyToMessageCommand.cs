using A3Nest.Domain.Enums;

namespace A3Nest.Application.Commands.Messages;

public class ReplyToMessageCommand
{
    public int ParentMessageId { get; set; }
    public int SenderId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public MessageType MessageType { get; set; } = MessageType.User;
    public List<string> Attachments { get; set; } = new();
}