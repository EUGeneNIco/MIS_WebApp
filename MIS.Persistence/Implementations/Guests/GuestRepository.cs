using Microsoft.EntityFrameworkCore;
using MIS.Application._Interfaces.Guests;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Persistence.Implementations.Guests
{
    public class GuestRepository : IGuestRepository
    {
        private readonly AppDbContext dbContext;

        public GuestRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private IQueryable<Guest> GetEntity()
        {
            return dbContext.Guests
                .Include(x => x.Network)
                .AsQueryable();
        }

        private IQueryable<Guest> GetActiveEntity()
        {
            return GetEntity().Where(x => !x.IsDeleted);
        }

        public async Task<Guest?> GetGuest(long id)
        {
            return await GetActiveEntity().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<IQueryable<Guest>> GetGuests()
        {
            return Task.FromResult(GetActiveEntity());
        }
    }
}
