using AFPMBAI.CLAIMS.Application.Exceptions;
using AFPMBAI.CLAIMS.Application.KIABenefits.Queries.GetKIABenefitsGrid;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Helpers;
using MIS.Application.Members.Queries.GetGuest;
using MIS.Domain;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Members.Queries.GetGuestGrid
{
    public class GetGuestGridQueryHandler : IRequestHandler<GetGuestGridQuery, GuestGridViewModel>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public GetGuestGridQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<GuestGridViewModel> Handle(GetGuestGridQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Guests
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            var data = new GuestGridViewModel
            {
                TotalDataCount = query.Count()
            };

            data.FilteredDataCount = query.Count();

            //Filter
            var name = QueryHelper.GetFilterValue(request.Filters, "name");
            if (!String.IsNullOrEmpty(name))
            {
                name = name.Trim();
                query = query.Where(x => x.FirstName.Contains(name) || x.MiddleName.Contains(name) || x.LastName.Contains(name));
            }

            //Sort
            if (request.SortKey == "network")
            {
                query = request.SortDirection == SortDirection.Ascending ? query.OrderBy(x => x.NetworkId)
                    : query.OrderByDescending(x => x.NetworkId);
            }

            //Page
            query = request.Limit > 0
                ? query
                    .Skip(request.Offset)
                    .Take(request.Limit)
                : query;

            data.Data = this.mapper.Map<IEnumerable<GuestGridItem>>(await query.ToListAsync(cancellationToken));

            return data;
        }
    }
}
