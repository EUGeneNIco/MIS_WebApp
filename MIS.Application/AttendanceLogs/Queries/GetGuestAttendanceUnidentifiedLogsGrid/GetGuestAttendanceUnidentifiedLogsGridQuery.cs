using MediatR;
using MIS.Application._ViewModels;

namespace MIS.Application.AttendanceLogs.Queries.GetGuestAttendanceUnidentifiedLogsGrid
{
    public class GetGuestAttendanceUnidentifiedLogsGridQuery : GridViewQuery, IRequest<GuestAttendanceUnidentifiedLogsGridViewModel>
    {
    }
}
