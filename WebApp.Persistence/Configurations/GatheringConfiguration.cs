using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Entities;
using WebApp.Infastructure.Constants;

namespace WebApp.Infastructure.Configurations;

internal sealed class GatheringConfiguration : IEntityTypeConfiguration<Gathering>
{
    public void Configure(EntityTypeBuilder<Gathering> builder)
    {
        builder.ToTable(TableNames.Gatherings);

        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Creator)
            .WithMany();

        builder
            .HasMany(x => x.Invitations)
            .WithOne()
            .HasForeignKey(x => x.GatheringId);

        builder
            .HasMany(x => x.Attendees)
            .WithOne()
            .HasForeignKey(x => x.GatheringId);
    }
}
