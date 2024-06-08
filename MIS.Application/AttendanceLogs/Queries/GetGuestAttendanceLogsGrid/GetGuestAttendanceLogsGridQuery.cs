using MediatR;
using MIS.Application._ViewModels;

namespace MIS.Application.AttendanceLogs.Queries.GetGuestAttendanceLogsGrid
{
    public class GetGuestAttendanceLogsGridQuery : GridViewQuery, IRequest<GuestAttendanceLogsGridViewModel>
    {
    }
}
