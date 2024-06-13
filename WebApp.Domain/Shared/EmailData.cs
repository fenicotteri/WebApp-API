
namespace WebApp.Infrastructure.Services;

public sealed record EmailData(
    string EmailToId,
    string EmailToName,
    string EmailSubject,
    string EmailBody);

