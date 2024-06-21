using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MIS.Domain.Entities;

namespace MIS.Domain
{
    public interface IAppDbContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<GuestEventRecord> GuestEventRecords { get; set; }
        DbSet<MemberEventRecord> MemberEventRecords { get; set; }
        DbSet<Guest> Guests { get; set; }
        DbSet<GuestAttendanceLog> GuestAttendanceLogs { get; set; }
        DbSet<GuestAttendanceUnidentifiedLog> GuestAttendanceUnidentifiedLogs { get; set; }
        DbSet<Member> Members { get; set; }
        DbSet<MemberAttendanceLog> MemberAttendanceLogs { get; set; }
        DbSet<MemberAttendanceUnidentifiedLog> MemberAttendanceUnidentifiedLogs { get; set; }
        DbSet<Network> Networks { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Service> Services { get; set; }

        /******************************************************************************/

        EntityEntry Remove(object entity);

        void RemoveRange(IEnumerable<object> entities);

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
