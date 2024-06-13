
using WebApp.Domain.Entities;
using WebApp.Application.Abstractions;
using MimeKit;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using WebApp.Domain.Exceptions;
using static System.Net.Mime.MediaTypeNames;

namespace WebApp.Infrastructure.Services;
internal sealed class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
    }

    public async Task SendInvitationAcceptedEmailAsync(Gathering gathering, EmailData emailData, CancellationToken cancellationToken = default)
    {
        
        try
        {

            MimeMessage emailMessage = new();
            MailboxAddress emailFrom = new(_emailSettings.Name, _emailSettings.EmailId);
            emailMessage.From.Add(emailFrom);

            MailboxAddress emailTo = new(emailData.EmailToName, emailData.EmailToId);
            emailMessage.To.Add(emailTo);

            emailMessage.Subject = emailData.EmailSubject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailData.EmailBody
            };

            using (var emailClient = new SmtpClient())
            {
                await emailClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL, cancellationToken);
                await emailClient.AuthenticateAsync(_emailSettings.EmailId, _emailSettings.Password, cancellationToken);
                await emailClient.SendAsync(emailMessage, cancellationToken);
                await emailClient.DisconnectAsync(true, cancellationToken);
            }
        }
        catch 
        {
            throw new EmailSendingWasUnsuccessful("Something went wrong while sending the email.");
        }
    }
        
}
