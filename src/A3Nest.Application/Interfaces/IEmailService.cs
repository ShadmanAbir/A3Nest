using System.Threading.Tasks;

namespace A3Nest.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendEmailAsync(string to, string from, string subject, string body);
        Task SendBulkEmailAsync(IEnumerable<string> recipients, string subject, string body);
        Task SendTemplateEmailAsync(string to, string templateId, object templateData);
        Task SendWelcomeEmailAsync(string to, string userName);
        Task SendLeaseReminderEmailAsync(string to, string tenantName, DateTime leaseExpiryDate);
        Task SendMaintenanceNotificationEmailAsync(string to, string propertyAddress, string maintenanceDetails);
        Task SendPaymentReminderEmailAsync(string to, string tenantName, decimal amount, DateTime dueDate);
    }
}