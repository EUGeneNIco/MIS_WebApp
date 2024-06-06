using MediatR;
using MIS.Application._ViewModels;

namespace MIS.Application.AttendanceLogs.Queries.GetMemberAttendanceLogsGrid
{
    public class GetMemberAttendanceLogsGridQuery : GridViewQuery, IRequest<MemberAttendanceLogsGridViewModel>
    {
    }
}
