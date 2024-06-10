
using WebApp.Domain.Entities;

namespace WebApp.Domain.Repositories;

public interface IAttendeeRepository
{
    void Add(Attendee attendee);
}
