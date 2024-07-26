using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities;

namespace MIS.Persistence.Configurations
{
    public class NetworkConfiguration : IEntityTypeConfiguration<Network>
    {
        public void Configure(EntityTypeBuilder<Network> builder)
        {
            builder.Property(u => u.Name)
                .HasMaxLength(50);
        }
    }
}
