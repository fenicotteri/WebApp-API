
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApp.Application.Abstractions.Services;
using WebApp.Domain.Entities;

namespace WebApp.Infrastructure.Services.Auth;

internal sealed class JwtProviderService : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProviderService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(Member member)
    {

        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, member.Email.Value)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
