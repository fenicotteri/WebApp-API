namespace WebApp.Infrastructure.Services.Email;

public class EmailOptions
{
    public string Name { get; init; } = string.Empty;
    public string EmailId { get; init; } = string.Empty;
    public string Host { get; init; } = string.Empty;
    public int Port { get; init; }
    public bool UseSSL { get; init; }
    public string Password { get; init; } = string.Empty;
}
