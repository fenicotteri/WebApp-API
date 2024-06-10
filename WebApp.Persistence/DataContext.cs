
using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Entities;

namespace WebApp.Persistence;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Member> Members { get; set; }
    public DbSet<Gathering> Gatherings { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<Attendee> Attendee { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}


