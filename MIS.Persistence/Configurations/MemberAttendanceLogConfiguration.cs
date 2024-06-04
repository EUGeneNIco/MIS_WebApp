using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities;

namespace MIS.Persistence.Configurations
{
    public class MemberAttendanceLogConfiguration : IEntityTypeConfiguration<MemberAttendanceLog>
    {
        public void Configure(EntityTypeBuilder<MemberAttendanceLog> builder)
        {
            builder.Property(x => x.MemberId)
                .IsRequired();
        }
    }
}
