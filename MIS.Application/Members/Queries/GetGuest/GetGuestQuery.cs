using MediatR;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Members.Queries.GetGuest
{
    public class GetGuestQuery : IRequest<GuestDto>
    {
        public long Id { get; set; }
    }
}
