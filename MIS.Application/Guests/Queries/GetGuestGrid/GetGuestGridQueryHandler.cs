using AutoMapper;
using MediatR;
using MIS.Application._Enums;
using MIS.Application._Helpers;
using MIS.Application._Interfaces;
using MIS.Domain.Entities;

namespace MIS.Application.Guests.Queries.GetGuestGrid
{
    public class GetGuestGridQueryHandler : IRequestHandler<GetGuestGridQuery, GuestGridViewModel>
    {
        private readonly IMapper mapper;
        private readonly IRepository<Guest> repository;

        public GetGuestGridQueryHandler(IMapper mapper, IRepository<Guest> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<GuestGridViewModel> Handle(GetGuestGridQuery request, CancellationToken cancellationToken)
        {
            var query = await repository.GetAllAsync();

            var data = new GuestGridViewModel
            {
                TotalDataCount = query.Count()
            };

            //Filter
            var name = QueryHelper.GetFilterValue(request.Filters, "name");
            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToUpper().Trim();
                query = query.Where(x => x.FirstName.ToUpper().Contains(name) || x.MiddleName.ToUpper().Contains(name) || x.LastName.ToUpper().Contains(name));
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

            data.Data = mapper.Map<IEnumerable<GuestGridItem>>(query);

            return data;
        }
    }
}
