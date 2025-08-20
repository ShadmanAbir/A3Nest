namespace A3Nest.Presentation.Services;

public interface IErrorHandlingService
{
    /// <summary>
    /// Handles unhandled exceptions globally
    /// </summary>
    /// <param name="exception">The exception to handle</param>
    /// <param name="context">Additional context about where the error occurred</param>
    Task HandleExceptionAsync(Exception exception, string context = "");

    /// <summary>
    /// Logs an error without displaying it to the user
    /// </summary>
    /// <param name="exception">The exception to log</param>
    /// <param name="context">Additional context</param>
    void LogError(Exception exception, string context = "");

    /// <summary>
    /// Shows a user-friendly error message
    /// </summary>
    /// <param name="message">The message to display</param>
    /// <param name="title">The title of the error dialog</param>
    Task ShowErrorAsync(string message, string title = "Error");

    /// <summary>
    /// Gets a user-friendly error message from an exception
    /// </summary>
    /// <param name="exception">The exception</param>
    /// <returns>A user-friendly error message</returns>
    string GetUserFriendlyMessage(Exception exception);
}