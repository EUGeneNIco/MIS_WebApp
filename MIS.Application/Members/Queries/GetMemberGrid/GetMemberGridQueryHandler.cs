using AutoMapper;
using MediatR;
using MIS.Application._Enums;
using MIS.Application._Helpers;
using MIS.Application._Interfaces;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Queries.GetMemberGrid
{
    public class GetMemberGridQueryHandler : IRequestHandler<GetMemberGridQuery, MemberGridViewModel>
    {
        private readonly IRepository<Member> memberRepository;
        private readonly IMapper mapper;

        public GetMemberGridQueryHandler(IRepository<Member> memberRepository, IMapper mapper)
        {
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }
        public async Task<MemberGridViewModel> Handle(GetMemberGridQuery request, CancellationToken cancellationToken)
        {
            var query = await memberRepository.GetAllAsync();

            var data = new MemberGridViewModel
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
                //query = request.SortDirection == SortDirection.Ascending
                //    ? query.OrderBy(x => x.NetworkId)
                //    : query.OrderByDescending(x => x.NetworkId);
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

            data.Data = mapper.Map<IEnumerable<MemberGridItem>>(query);

            return data;
        }
    }
}
