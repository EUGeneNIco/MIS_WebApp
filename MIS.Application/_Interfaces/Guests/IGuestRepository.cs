using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application._Interfaces.Guests
{
    public interface IGuestRepository
    {
        Task<Guest?> GetGuest(long id);
        Task<IQueryable<Guest>> GetGuests();
    }
}
