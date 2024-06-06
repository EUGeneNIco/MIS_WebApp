using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Helpers;
using MIS.Domain;

namespace MIS.Application.AttendanceLogs.Queries.GetMemberAttendanceLogsGrid
{
    public class GetMemberAttendanceLogsGridQueryHandler : IRequestHandler<GetMemberAttendanceLogsGridQuery, MemberAttendanceLogsGridViewModel>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public GetMemberAttendanceLogsGridQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<MemberAttendanceLogsGridViewModel> Handle(GetMemberAttendanceLogsGridQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.MemberAttendanceLogs.AsQueryable();

            var data = new MemberAttendanceLogsGridViewModel
            {
                TotalDataCount = query.Count()
            };

            //Filter
            var name = QueryHelper.GetFilterValue(request.Filters, "member");
            var logDateTime = QueryHelper.GetFilterValue(request.Filters, "logDateTime");
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                query = query.Where(x => x.Member.FirstName.Contains(name) || x.Member.MiddleName.Contains(name) || x.Member.LastName.Contains(name));
            }
            if (!string.IsNullOrEmpty(logDateTime))
            {
                var date = DateTime.Parse(logDateTime);
                query = query.Where(x => x.LogDateTime.Date == date);
            }

            data.FilteredDataCount = query.Count();

            //Sort
            if (request.SortKey == "logDateTime")
            {
                query = request.SortDirection == SortDirection.Ascending ? query.OrderBy(x => x.LogDateTime)
                    : query.OrderByDescending(x => x.LogDateTime);
            }

            //Page
            query = request.Limit > 0
                ? query
                    .Skip(request.Offset)
                    .Take(request.Limit)
                : query;

            data.Data = mapper.Map<IEnumerable<MemberAttendanceLogsGridItem>>(await query.ToListAsync(cancellationToken));

            return data;
        }
    }
}
