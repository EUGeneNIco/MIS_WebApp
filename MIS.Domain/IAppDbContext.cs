using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MIS.Domain.Entities;

namespace MIS.Domain
{
    public interface IAppDbContext
    {
        DbSet<Guest> Guests { get; set; }
        DbSet<Member> Members { get; set; }
        DbSet<Network> Networks { get; set; }
        DbSet<User> Users { get; set; }

        /******************************************************************************/

        EntityEntry Remove(object entity);

        void RemoveRange(IEnumerable<object> entities);

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
