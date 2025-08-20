using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Messages;
using A3Nest.Application.Queries.Messages;
using A3Nest.Application.Interfaces;

namespace A3Nest.Application.Services;

public class MessageService : IMessageService
{
    public Task<MessageDto> GetMessageAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MessageDto>> GetMessagesAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MessageDto>> GetSentMessagesAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MessageDto>> GetReceivedMessagesAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MessageDto>> GetUnreadMessagesAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<MessageDto> SendMessageAsync(SendMessageCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<MessageDto> ReplyToMessageAsync(ReplyToMessageCommand command)
    {
        throw new NotImplementedException();
    }

    public Task MarkMessageAsReadAsync(int messageId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMessageAsync(int messageId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MessageDto>> SearchMessagesAsync(SearchMessagesQuery query)
    {
        throw new NotImplementedException();
    }
}