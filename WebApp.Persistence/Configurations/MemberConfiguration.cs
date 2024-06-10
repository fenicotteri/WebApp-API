using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Domain.Entities;
using WebApp.Domain.ValueObjects;
using WebApp.Infastructure.Constants;

namespace WebApp.Infastructure.Configurations;
internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable(TableNames.Members);

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Email)
            .HasConversion(x => x.Value, v => Email.Create(v).Value);

        builder.HasIndex(x => x.Email).IsUnique();

        builder
            .Property(x => x.FirstName)
            .HasConversion(x => x.Value, v => FirstName.Create(v).Value)
            .HasMaxLength(FirstName.MaxLength);

        builder
            .Property(x => x.LastName)
            .HasConversion(x => x.Value, v => LastName.Create(v).Value)
            .HasMaxLength(LastName.MaxLength);
    }
}
