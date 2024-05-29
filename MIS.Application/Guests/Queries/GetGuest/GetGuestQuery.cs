using MediatR;
using MIS.Application.Guests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Guests.Queries.GetGuest
{
    public class GetGuestQuery : IRequest<GuestDto>
    {
        public long Id { get; set; }
    }
}
