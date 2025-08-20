using Microsoft.AspNetCore.SignalR;

namespace A3Nest.Infrastructure.Hubs;

/// <summary>
/// SignalR hub for real-time messaging functionality
/// </summary>
public class MessageHub : Hub
{
    /// <summary>
    /// Sends a message to a specific user
    /// </summary>
    /// <param name="userId">Target user ID</param>
    /// <param name="message">Message content</param>
    public async Task SendMessage(string userId, string message)
    {
        throw new NotImplementedException("SendMessage functionality will be implemented in future iterations");
    }

    /// <summary>
    /// Joins a user to a specific group for group messaging
    /// </summary>
    /// <param name="groupName">Name of the group to join</param>
    public async Task JoinGroup(string groupName)
    {
        throw new NotImplementedException("JoinGroup functionality will be implemented in future iterations");
    }

    /// <summary>
    /// Leaves a specific group
    /// </summary>
    /// <param name="groupName">Name of the group to leave</param>
    public async Task LeaveGroup(string groupName)
    {
        throw new NotImplementedException("LeaveGroup functionality will be implemented in future iterations");
    }
}