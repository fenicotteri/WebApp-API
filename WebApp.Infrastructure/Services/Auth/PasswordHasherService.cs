
using WebApp.Application.Abstractions.Services;

namespace WebApp.Infrastructure.Services.Auth;

internal sealed class PasswordHasherService : IPasswordHasher
{
    public string GeneratePassword(string password) => 
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool VerifyPassword(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);

}
