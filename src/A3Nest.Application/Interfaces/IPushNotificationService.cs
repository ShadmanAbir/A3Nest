using System.Threading.Tasks;

namespace A3Nest.Application.Interfaces
{
    public interface IPushNotificationService
    {
        Task SendNotificationAsync(string deviceToken, string title, string message);
        Task SendNotificationAsync(string deviceToken, string title, string message, Dictionary<string, string> data);
        Task SendBulkNotificationAsync(IEnumerable<string> deviceTokens, string title, string message);
        Task SendNotificationToUserAsync(string userId, string title, string message);
        Task SendNotificationToTopicAsync(string topic, string title, string message);
        Task SubscribeToTopicAsync(string deviceToken, string topic);
        Task UnsubscribeFromTopicAsync(string deviceToken, string topic);
        Task SendLeaseExpiryNotificationAsync(string userId, string propertyName, DateTime expiryDate);
        Task SendMaintenanceUpdateNotificationAsync(string userId, string taskDescription, string status);
        Task SendPaymentDueNotificationAsync(string userId, decimal amount, DateTime dueDate);
    }
}