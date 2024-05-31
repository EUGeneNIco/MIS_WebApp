using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Helpers;
using MIS.Domain;

namespace MIS.Application.Members.Queries.GetMemberGrid
{
    public class GetMemberGridQueryHandler : IRequestHandler<GetMemberGridQuery, MemberGridViewModel>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public GetMemberGridQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<MemberGridViewModel> Handle(GetMemberGridQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Members
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            var data = new MemberGridViewModel
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

            data.Data = mapper.Map<IEnumerable<MemberGridItem>>(await query.ToListAsync(cancellationToken));

            return data;
        }
    }
}
