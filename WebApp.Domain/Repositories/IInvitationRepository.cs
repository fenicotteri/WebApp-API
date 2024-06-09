
using WebApp.Domain.Entities;

namespace WebApp.Domain.Repositories;

public interface IInvitationRepository
{
    void Add(Invitation invitation);
}
