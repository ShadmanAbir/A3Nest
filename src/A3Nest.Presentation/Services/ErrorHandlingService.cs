using Microsoft.Extensions.Logging;

namespace A3Nest.Presentation.Services;

public class ErrorHandlingService : IErrorHandlingService
{
    private readonly ILogger<ErrorHandlingService> _logger;

    public ErrorHandlingService(ILogger<ErrorHandlingService> logger)
    {
        _logger = logger;
    }

    public async Task HandleExceptionAsync(Exception exception, string context = "")
    {
        // Log the exception
        LogError(exception, context);

        // Show user-friendly message
        var userMessage = GetUserFriendlyMessage(exception);
        await ShowErrorAsync(userMessage);
    }

    public void LogError(Exception exception, string context = "")
    {
        _logger.LogError(exception, "Error occurred in context: {Context}", context);
    }

    public async Task ShowErrorAsync(string message, string title = "Error")
    {
        try
        {
            if (Microsoft.Maui.Controls.Application.Current?.MainPage != null)
            {
                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(title, message, "OK");
            }
        }
        catch (Exception ex)
        {
            // Fallback logging if we can't show the alert
            _logger.LogError(ex, "Failed to display error alert: {Message}", message);
        }
    }

    public string GetUserFriendlyMessage(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => "A required value was not provided. Please check your input and try again.",
            ArgumentException => "Invalid input provided. Please check your data and try again.",
            InvalidOperationException => "This operation cannot be performed at this time. Please try again later.",
            UnauthorizedAccessException => "You don't have permission to perform this action.",
            TimeoutException => "The operation timed out. Please check your connection and try again.",
            HttpRequestException => "Network error occurred. Please check your internet connection and try again.",
            NotImplementedException => "This feature is not yet implemented.",
            _ => "An unexpected error occurred. Please try again or contact support if the problem persists."
        };
    }
}