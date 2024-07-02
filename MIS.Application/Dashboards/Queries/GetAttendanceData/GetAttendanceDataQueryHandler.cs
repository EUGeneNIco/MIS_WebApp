using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Dashboards.Models;
using MIS.Domain;

namespace MIS.Application.Dashboards.Queries.GetAttendanceData
{
    public class GetAttendanceDataQueryHandler : IRequestHandler<GetAttendanceDataQuery, AttendanceDataResponse>
    {
        private readonly IAppDbContext _dbContext;

        public GetAttendanceDataQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AttendanceDataResponse> Handle(GetAttendanceDataQuery request, CancellationToken cancellationToken)
        {
            var eventDates = await _dbContext.MemberAttendanceLogs
                .OrderByDescending(x => x.LogDateTime.Date)
                .Select(x => x.LogDateTime.Date)
                .Distinct()
                .Take(7)
                .ToListAsync(cancellationToken);

            var dataSets = new List<AttendanceDataSet>();
            foreach (var date in eventDates)
            {
                var memberIds = await _dbContext.MemberAttendanceLogs
                    .Where(x => x.LogDateTime.Date == date)
                    .Select(x => x.MemberId)
                    .Distinct()
                    .ToListAsync(cancellationToken);

                var guestIds = await _dbContext.GuestAttendanceLogs
                    .Where(x => x.LogDateTime.Date == date)
                    .Select(x => x.GuestId)
                    .Distinct()
                    .ToListAsync(cancellationToken);

                dataSets.Add(new AttendanceDataSet
                {
                    MembersCount = memberIds.Count,
                    GuestsCount = guestIds.Count,
                    DateTime = date
                });
            }

            return new AttendanceDataResponse { Datasets = dataSets };
        }
    }
}
