namespace WebApp.Application.Abstractions.Services;

public interface IPasswordHasher
{
    string GeneratePassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
