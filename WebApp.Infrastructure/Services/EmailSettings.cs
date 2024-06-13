
namespace WebApp.Infrastructure.Services;

public class EmailSettings
{
    public string Name { get; set; }
    public string EmailId { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public bool UseSSL { get; set; }
    public string Password { get; set; }
}