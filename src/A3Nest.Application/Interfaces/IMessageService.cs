using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Messages;
using A3Nest.Application.Queries.Messages;

namespace A3Nest.Application.Interfaces;

public interface IMessageService
{
    Task<MessageDto> GetMessageAsync(int id);
    Task<IEnumerable<MessageDto>> GetMessagesAsync(int userId);
    Task<IEnumerable<MessageDto>> GetSentMessagesAsync(int userId);
    Task<IEnumerable<MessageDto>> GetReceivedMessagesAsync(int userId);
    Task<IEnumerable<MessageDto>> GetUnreadMessagesAsync(int userId);
    Task<MessageDto> SendMessageAsync(SendMessageCommand command);
    Task<MessageDto> ReplyToMessageAsync(ReplyToMessageCommand command);
    Task MarkMessageAsReadAsync(int messageId, int userId);
    Task DeleteMessageAsync(int messageId, int userId);
    Task<IEnumerable<MessageDto>> SearchMessagesAsync(SearchMessagesQuery query);
}