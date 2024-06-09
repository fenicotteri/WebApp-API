
using WebApp.Domain.Entities;
using WebApp.Domain.Repositories;

namespace WebApp.Persistence.Repositories;

public class InvitationRepository : IInvitationRepository
{
    private readonly DataContext _dbContext;
    public InvitationRepository(DataContext dbContext) => _dbContext = dbContext;
    public void Add(Invitation invitation)
    {
        _dbContext.Invitations.Add(invitation);
    }
}
