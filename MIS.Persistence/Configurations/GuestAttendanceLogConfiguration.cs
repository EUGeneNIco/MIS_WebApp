using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities;

namespace MIS.Persistence.Configurations
{
    public class GuestAttendanceLogConfiguration : IEntityTypeConfiguration<GuestAttendanceLog>
    {
        public void Configure(EntityTypeBuilder<GuestAttendanceLog> builder)
        {
            builder.Property(x => x.GuestId)
                .IsRequired();
        }
    }
}
