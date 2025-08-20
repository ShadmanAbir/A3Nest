using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Messages;
using A3Nest.Application.Queries.Messages;

namespace A3Nest.Infrastructure.Repositories;

public class MessageRepository : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;

    public MessageRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageDto> GetMessageAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MessageDto>> GetMessagesAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MessageDto>> GetSentMessagesAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MessageDto>> GetReceivedMessagesAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MessageDto>> GetUnreadMessagesAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<MessageDto> SendMessageAsync(SendMessageCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<MessageDto> ReplyToMessageAsync(ReplyToMessageCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task MarkMessageAsReadAsync(int messageId, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteMessageAsync(int messageId, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MessageDto>> SearchMessagesAsync(SearchMessagesQuery query)
    {
        throw new NotImplementedException();
    }
}