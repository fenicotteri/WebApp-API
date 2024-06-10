
using WebApp.Domain.Entities;
using WebApp.Domain.Repositories;

namespace WebApp.Persistence.Repositories;

public class AttendeeRepository : IAttendeeRepository
{
    private readonly DataContext _dbContext;

    public AttendeeRepository(DataContext dbContext) => _dbContext = dbContext; 
    public void Add(Attendee attendee)
    {
        _dbContext.Attendee.Add(attendee);
    }
}
