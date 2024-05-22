using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain
{
    public interface IAppDbContext
    {
        DbSet<Member> Members { get; set; }
        DbSet<Network> Networks { get; set; }

        /******************************************************************************/

        EntityEntry Remove(object entity);

        void RemoveRange(IEnumerable<object> entities);

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
