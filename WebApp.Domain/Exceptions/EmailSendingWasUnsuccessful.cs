
namespace WebApp.Domain.Exceptions;

public sealed class EmailSendingWasUnsuccessful : Exception
{
    public EmailSendingWasUnsuccessful(string message)
        : base(message)
    {
    }
}
