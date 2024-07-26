using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities;

namespace MIS.Persistence.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(u => u.Name)
                .HasMaxLength(50);
        }
    }
}
