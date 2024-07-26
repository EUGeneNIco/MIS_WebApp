using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities;

namespace MIS.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserName)
                .HasMaxLength(50);
            builder.Property(u => u.FirstName)
                .HasMaxLength(50);
            builder.Property(u => u.MiddleName)
                .HasMaxLength(50);
            builder.Property(u => u.LastName)
                .HasMaxLength(50);
            builder.Property(u => u.Role)
                .HasMaxLength(50);
            builder.Property(u => u.PasswordHash)
                .HasMaxLength(100);
        }
    }
}
