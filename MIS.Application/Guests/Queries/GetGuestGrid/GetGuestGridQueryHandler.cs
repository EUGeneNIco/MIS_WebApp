using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Helpers;
using MIS.Application._Interfaces.Guests;
using MIS.Domain;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Guests.Queries.GetGuestGrid
{
    public class GetGuestGridQueryHandler : IRequestHandler<GetGuestGridQuery, GuestGridViewModel>
    {
        private readonly IGuestRepository guestRepository;
        private readonly IMapper mapper;

        public GetGuestGridQueryHandler(IGuestRepository guestRepository, IMapper mapper)
        {
            this.guestRepository = guestRepository;
            this.mapper = mapper;
        }
        public async Task<GuestGridViewModel> Handle(GetGuestGridQuery request, CancellationToken cancellationToken)
        {
            var query = await guestRepository.GetGuests();

            var data = new GuestGridViewModel
            {
                TotalDataCount = query.Count()
            };

            //Filter
            var name = QueryHelper.GetFilterValue(request.Filters, "name");
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                query = query.Where(x => x.FirstName.Contains(name) || x.MiddleName.Contains(name) || x.LastName.Contains(name));
            }

            data.FilteredDataCount = query.Count();

            //Sort
            if (request.SortKey == "network")
            {
                query = request.SortDirection == SortDirection.Ascending ? query.OrderBy(x => x.NetworkId)
                    : query.OrderByDescending(x => x.NetworkId);
            }
            else if (request.SortKey == "name")
            {
                query = request.SortDirection == SortDirection.Ascending ? query.OrderBy(x => x.LastName)
                    : query.OrderByDescending(x => x.LastName);
            }

            //Page
            query = request.Limit > 0
                ? query
                    .Skip(request.Offset)
                    .Take(request.Limit)
                : query;

            data.Data = mapper.Map<IEnumerable<GuestGridItem>>(await query.ToListAsync(cancellationToken));

            return data;
        }
    }
}
