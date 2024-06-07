using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Helpers;
using MIS.Domain;

namespace MIS.Application.AttendanceLogs.Queries.GetMemberAttendanceUnidentifiedLogsGrid
{
    public class GetMemberAttendanceUnidentifiedLogsGridQueryHandler : IRequestHandler<GetMemberAttendanceUnidentifiedLogsGridQuery, MemberAttendanceUnidentifiedLogsGridViewModel>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public GetMemberAttendanceUnidentifiedLogsGridQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<MemberAttendanceUnidentifiedLogsGridViewModel> Handle(GetMemberAttendanceUnidentifiedLogsGridQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.MemberAttendanceUnidentifiedLogs.AsQueryable();

            var data = new MemberAttendanceUnidentifiedLogsGridViewModel
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

            data.Data = mapper.Map<IEnumerable<MemberAttendanceUnidentifiedLogsGridItem>>(await query.ToListAsync(cancellationToken));

            return data;
        }
    }
}
