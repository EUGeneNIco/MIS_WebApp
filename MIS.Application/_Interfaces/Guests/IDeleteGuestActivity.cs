using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application._Interfaces.Guests
{
    public interface IDeleteGuestActivity
    {
        Task Execute(Guest guest, CancellationToken cancellation);
    }
}
