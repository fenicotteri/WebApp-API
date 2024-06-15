
using WebApp.Domain.Entities;

namespace WebApp.Application.Abstractions.Services;

public interface IJwtProvider
{
    string Generate(Member member);
}
