using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities;

namespace MIS.Persistence.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.Property(b => b.Code)
                .HasMaxLength(100);
            builder.Property(b => b.FirstName)
                .HasMaxLength(50);
            builder.Property(b => b.MiddleName)
                .HasMaxLength(50);
            builder.Property(b => b.LastName)
                .HasMaxLength(50);
            builder.Property(b => b.Address)
                .HasMaxLength(200);
            builder.Property(b => b.Gender)
                .HasMaxLength(50);
            builder.Property(b => b.ContactNumber)
                .HasMaxLength(50);
            builder.Property(b => b.CivilStatus)
                .HasMaxLength(50);
            builder.Property(b => b.Extension)
                .HasMaxLength(50);
        }
    }
}
