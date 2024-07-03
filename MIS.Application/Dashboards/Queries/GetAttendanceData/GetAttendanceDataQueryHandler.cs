using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Interfaces;
using MIS.Application.Dashboards.Models;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Application.Dashboards.Queries.GetAttendanceData
{
    public class GetAttendanceDataQueryHandler : IRequestHandler<GetAttendanceDataQuery, AttendanceDataResponse>
    {
        private readonly IRepository<MemberAttendanceLog> _memberAttendanceLogsRepository;
        private readonly IRepository<GuestAttendanceLog> _guestAttendanceLogRepository;

        public GetAttendanceDataQueryHandler(IRepository<MemberAttendanceLog> memberAttendanceLogsRepository, IRepository<GuestAttendanceLog> guestAttendanceLogRepository)
        {
            _memberAttendanceLogsRepository = memberAttendanceLogsRepository;
            _guestAttendanceLogRepository = guestAttendanceLogRepository;
        }

        public async Task<AttendanceDataResponse> Handle(GetAttendanceDataQuery request, CancellationToken cancellationToken)
        {
            var eventDates = _memberAttendanceLogsRepository.GetAllQuery()
                .Select(x => x.LogDateTime.Date)
                .Distinct()
                .OrderByDescending(x => x)
                .Take(7)
                .Reverse()
                .ToList();

            var dataSets = new List<AttendanceDataSet>();
            foreach (var date in eventDates)
            {
                var memberIds = await _memberAttendanceLogsRepository.GetAllQuery()
                    .Where(x => x.LogDateTime.Date == date)
                    .Select(x => x.MemberId)
                    .Distinct()
                    .ToListAsync(cancellationToken);

                var guestIds = await _guestAttendanceLogRepository.GetAllQuery()
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
