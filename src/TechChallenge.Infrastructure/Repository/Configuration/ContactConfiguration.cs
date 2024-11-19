using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Infrastructure.Repository.Configuration;
internal class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
  public void Configure(EntityTypeBuilder<Contact> builder)
  {
    builder.ToTable("Contact");
    builder.HasKey(p => p.Guid);
    builder.Property(p => p.Guid)
        .HasColumnType("UNIQUEIDENTIFIER"); ;
    builder.Property(P => P.Phone)
        .HasColumnType("VARCHAR(10)")
        .IsRequired();
    builder.Property(P => P.Name)
                .HasColumnType("VARCHAR(150)")
                .IsRequired();
    builder.Property(p => p.Email)
                .HasColumnType("VARCHAR(150)")
                .IsRequired();

    builder.HasOne(p => p.Region)
      .WithMany(x => x.Contacts)
      .HasConstraintName("FK_Contact_Region")
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasData(
    new Contact {Guid = Guid.NewGuid(), Name = "Joao", Email = "joao@gmail.com", Phone = "999988888", RegionDDD = 12 },
    new Contact { Guid = Guid.NewGuid(), Name = "Pedro", Email = "pedro@gmail.com", Phone = "999977778", RegionDDD = 19 },
    new Contact { Guid = Guid.NewGuid(), Name = "Paulo", Email = "paulo@gmail.com", Phone = "321456888", RegionDDD = 11 }
  );
  }
}
