using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Helpers;
using MIS.Domain;

namespace MIS.Application.AttendanceLogs.Queries.GetGuestAttendanceLogsGrid
{
    public class GetGuestAttendanceLogsGridQueryHandler : IRequestHandler<GetGuestAttendanceLogsGridQuery, GuestAttendanceLogsGridViewModel>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public GetGuestAttendanceLogsGridQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<GuestAttendanceLogsGridViewModel> Handle(GetGuestAttendanceLogsGridQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.GuestAttendanceLogs
                .OrderByDescending(x => x.LogDateTime)
                .AsQueryable();

            var data = new GuestAttendanceLogsGridViewModel
            {
                TotalDataCount = query.Count()
            };

            //Filter
            var name = QueryHelper.GetFilterValue(request.Filters, "name");
            var logDateTime = QueryHelper.GetFilterValue(request.Filters, "logDateTime");
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                query = query.Where(x => x.Guest.FirstName.Contains(name) || x.Guest.MiddleName.Contains(name) || x.Guest.LastName.Contains(name));
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

            data.Data = mapper.Map<IEnumerable<GuestAttendanceLogsGridItem>>(await query.ToListAsync(cancellationToken));

            return data;
        }
    }
}
