using MIS.Application._Interfaces.Guests;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Persistence.Implementations.Guests
{
    public class DeleteGuestActivity : IDeleteGuestActivity
    {
        private readonly AppDbContext dbContext;

        public DeleteGuestActivity(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Execute(Guest guest, CancellationToken cancellation)
        {
            guest.IsDeleted = true;
            await dbContext.SaveChangesAsync(cancellation);
        }
    }
}
