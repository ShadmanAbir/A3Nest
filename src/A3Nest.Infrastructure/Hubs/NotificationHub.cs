using Microsoft.AspNetCore.SignalR;

namespace A3Nest.Infrastructure.Hubs;

/// <summary>
/// SignalR hub for real-time notification functionality
/// </summary>
public class NotificationHub : Hub
{
    /// <summary>
    /// Sends a notification to a specific user
    /// </summary>
    /// <param name="userId">Target user ID</param>
    /// <param name="notification">Notification content</param>
    public async Task SendNotification(string userId, string notification)
    {
        throw new NotImplementedException("SendNotification functionality will be implemented in future iterations");
    }

    /// <summary>
    /// Sends a notification to all users in a specific group
    /// </summary>
    /// <param name="groupName">Target group name</param>
    /// <param name="notification">Notification content</param>
    public async Task SendGroupNotification(string groupName, string notification)
    {
        throw new NotImplementedException("SendGroupNotification functionality will be implemented in future iterations");
    }

    /// <summary>
    /// Sends a broadcast notification to all connected users
    /// </summary>
    /// <param name="notification">Notification content</param>
    public async Task SendBroadcastNotification(string notification)
    {
        throw new NotImplementedException("SendBroadcastNotification functionality will be implemented in future iterations");
    }
}