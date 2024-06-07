using MediatR;
using MIS.Application._ViewModels;

namespace MIS.Application.AttendanceLogs.Queries.GetMemberAttendanceUnidentifiedLogsGrid
{
    public class GetMemberAttendanceUnidentifiedLogsGridQuery : GridViewQuery, IRequest<MemberAttendanceUnidentifiedLogsGridViewModel>
    {
    }
}
