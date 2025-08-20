using A3Nest.Domain.Enums;

namespace A3Nest.Application.Queries.Messages;

public class SearchMessagesQuery
{
    public int UserId { get; set; }
    public string? SearchTerm { get; set; }
    public string? Subject { get; set; }
    public int? SenderId { get; set; }
    public int? ReceiverId { get; set; }
    public MessageType? MessageType { get; set; }
    public bool? IsRead { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}