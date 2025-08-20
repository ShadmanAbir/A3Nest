using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using A3Nest.Application.Interfaces;
using A3Nest.Application.DTOs;
using A3Nest.Application.Commands.Messages;
using A3Nest.Application.Queries.Messages;
using A3Nest.Domain.Enums;
using A3Nest.Presentation.Services;
using System.Collections.ObjectModel;

namespace A3Nest.Presentation.ViewModels;

public partial class MessagingViewModel : BaseViewModel
{
    private readonly IMessageService _messageService;
    private readonly ISampleDataService _sampleDataService;

    public MessagingViewModel(IMessageService messageService, ISampleDataService sampleDataService)
    {
        _messageService = messageService;
        _sampleDataService = sampleDataService;
        Title = "Messages";
        
        Messages = new ObservableCollection<MessageDto>();
        FilteredMessages = new ObservableCollection<MessageDto>();
        Conversations = new ObservableCollection<ConversationDto>();
    }

    [ObservableProperty]
    private int currentUserId = 1; // Placeholder - would come from authentication

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private MessageDto? selectedMessage;

    [ObservableProperty]
    private ConversationDto? selectedConversation;

    [ObservableProperty]
    private bool isSearching;

    [ObservableProperty]
    private string selectedMessageType = "All";

    [ObservableProperty]
    private string selectedFolder = "Inbox";

    [ObservableProperty]
    private bool showUnreadOnly;

    [ObservableProperty]
    private string newMessageText = string.Empty;

    [ObservableProperty]
    private string newMessageSubject = string.Empty;

    [ObservableProperty]
    private int newMessageRecipientId;

    [ObservableProperty]
    private bool isComposingMessage;

    [ObservableProperty]
    private bool isSendingMessage;

    [ObservableProperty]
    private int unreadCount;

    [ObservableProperty]
    private int totalCount;

    public ObservableCollection<MessageDto> Messages { get; }
    public ObservableCollection<MessageDto> FilteredMessages { get; }
    public ObservableCollection<ConversationDto> Conversations { get; }

    public List<string> MessageTypeOptions { get; } = new()
    {
        "All",
        "System",
        "User",
        "Notification"
    };

    public List<string> FolderOptions { get; } = new()
    {
        "Inbox",
        "Sent",
        "Unread",
        "All Messages"
    };

    public override async Task LoadAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            ClearError();

            await LoadMessagesAsync();
            await LoadConversationsAsync();
            UpdateMessageCounts();
        }
        catch (Exception ex)
        {
            SetError($"Failed to load messages: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SearchMessagesAsync()
    {
        await SearchAsync();
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        if (IsSearching) return;

        try
        {
            IsSearching = true;
            ClearError();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                ApplyFilters();
                return;
            }

            // Simulate search delay
            await Task.Delay(100);
            
            // Apply search filter through existing filter mechanism
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Search failed: {ex.Message}");
        }
        finally
        {
            IsSearching = false;
        }
    }

    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = string.Empty;
        ApplyFilters();
    }

    [RelayCommand]
    private void StartComposingMessage()
    {
        IsComposingMessage = true;
        NewMessageText = string.Empty;
        NewMessageSubject = string.Empty;
        NewMessageRecipientId = 0;
    }

    [RelayCommand]
    private void NewMessage()
    {
        StartComposingMessage();
    }

    [RelayCommand]
    private void CancelComposingMessage()
    {
        IsComposingMessage = false;
        NewMessageText = string.Empty;
        NewMessageSubject = string.Empty;
        NewMessageRecipientId = 0;
    }

    [RelayCommand]
    private async Task SendMessageAsync()
    {
        if (IsSendingMessage || string.IsNullOrWhiteSpace(NewMessageText) || NewMessageRecipientId <= 0)
            return;

        try
        {
            IsSendingMessage = true;
            ClearError();

            // Placeholder implementation - would call actual service
            await Task.Delay(100); // Simulate async operation
            
            // In real implementation:
            // var command = new SendMessageCommand
            // {
            //     SenderId = CurrentUserId,
            //     ReceiverId = NewMessageRecipientId,
            //     Subject = NewMessageSubject,
            //     Content = NewMessageText,
            //     MessageType = MessageType.User
            // };
            // var sentMessage = await _messageService.SendMessageAsync(command);
            // Messages.Insert(0, sentMessage);

            CancelComposingMessage();
            await LoadAsync(); // Refresh messages
        }
        catch (Exception ex)
        {
            SetError($"Failed to send message: {ex.Message}");
        }
        finally
        {
            IsSendingMessage = false;
        }
    }

    [RelayCommand]
    private async Task ReplyToMessageAsync(MessageDto? message)
    {
        if (message == null) return;

        try
        {
            // Placeholder implementation - would call actual service
            await Task.Delay(100); // Simulate async operation
            
            // In real implementation:
            // var command = new ReplyToMessageCommand
            // {
            //     OriginalMessageId = message.Id,
            //     SenderId = CurrentUserId,
            //     Content = "Reply content here"
            // };
            // var reply = await _messageService.ReplyToMessageAsync(command);
            
            await LoadAsync(); // Refresh messages
        }
        catch (Exception ex)
        {
            SetError($"Failed to reply to message: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task MarkAsReadAsync(MessageDto? message)
    {
        if (message == null || message.IsRead) return;

        try
        {
            // Placeholder implementation - would call actual service
            // await _messageService.MarkMessageAsReadAsync(message.Id, CurrentUserId);
            
            message.IsRead = true;
            message.ReadAt = DateTime.UtcNow;
            UpdateMessageCounts();
        }
        catch (Exception ex)
        {
            SetError($"Failed to mark message as read: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task DeleteMessageAsync(MessageDto? message)
    {
        if (message == null) return;

        try
        {
            // Confirmation dialog would be shown here
            bool confirmed = await Shell.Current.DisplayAlert(
                "Delete Message", 
                "Are you sure you want to delete this message?", 
                "Yes", "No");

            if (!confirmed) return;

            // Placeholder implementation - would call actual service
            // await _messageService.DeleteMessageAsync(message.Id, CurrentUserId);
            
            Messages.Remove(message);
            FilteredMessages.Remove(message);
            UpdateMessageCounts();
        }
        catch (Exception ex)
        {
            SetError($"Failed to delete message: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task ViewMessageDetailsAsync(MessageDto? message)
    {
        if (message == null) return;
        
        SelectedMessage = message;
        
        // Mark as read if not already read
        if (!message.IsRead)
        {
            await MarkAsReadAsync(message);
        }
        
        // Navigation to message details page placeholder
        await Shell.Current.GoToAsync($"//messaging/details?id={message.Id}");
    }

    [RelayCommand]
    private async Task ViewConversationAsync(ConversationDto? conversation)
    {
        if (conversation == null) return;
        
        SelectedConversation = conversation;
        
        // Navigation to conversation view placeholder
        await Shell.Current.GoToAsync($"//messaging/conversation?id={conversation.Id}");
    }

    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            ApplyFilters();
        }
    }

    partial void OnSelectedMessageTypeChanged(string value)
    {
        ApplyFilters();
    }

    partial void OnSelectedFolderChanged(string value)
    {
        ApplyFilters();
    }

    partial void OnShowUnreadOnlyChanged(bool value)
    {
        ApplyFilters();
    }

    private async Task LoadMessagesAsync()
    {
        Messages.Clear();
        
        // Load sample messages data
        var messages = await _sampleDataService.GetSampleMessagesAsync();
        
        // Filter messages based on selected folder
        var filteredMessages = SelectedFolder switch
        {
            "Inbox" => messages.Where(m => m.ReceiverId == CurrentUserId),
            "Sent" => messages.Where(m => m.SenderId == CurrentUserId),
            "Unread" => messages.Where(m => m.ReceiverId == CurrentUserId && !m.IsRead),
            _ => messages
        };
        
        foreach (var message in filteredMessages)
        {
            Messages.Add(message);
        }
        
        ApplyFilters();
    }

    private async Task LoadConversationsAsync()
    {
        Conversations.Clear();
        
        // Placeholder implementation - would call actual service
        await Task.Delay(100); // Simulate async operation
        
        // In real implementation, would group messages by conversation
        // and create ConversationDto objects
    }

    private void UpdateMessageCounts()
    {
        UnreadCount = Messages.Count(m => !m.IsRead);
        TotalCount = Messages.Count;
    }

    private void ApplyFilters()
    {
        FilteredMessages.Clear();

        var filtered = Messages.AsEnumerable();

        // Apply message type filter
        if (SelectedMessageType != "All")
        {
            if (Enum.TryParse<MessageType>(SelectedMessageType, out var messageType))
            {
                filtered = filtered.Where(m => m.MessageType == messageType);
            }
        }

        // Apply unread filter
        if (ShowUnreadOnly)
        {
            filtered = filtered.Where(m => !m.IsRead);
        }

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(m => 
                m.Subject.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                m.Content.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                (m.Sender?.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        // Sort by most recent first
        filtered = filtered.OrderByDescending(m => m.CreatedAt);

        foreach (var message in filtered)
        {
            FilteredMessages.Add(message);
        }
    }
}

// Helper DTO for conversation grouping
public class ConversationDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public UserDto? OtherParticipant { get; set; }
    public MessageDto? LastMessage { get; set; }
    public int UnreadCount { get; set; }
    public DateTime LastActivity { get; set; }
}