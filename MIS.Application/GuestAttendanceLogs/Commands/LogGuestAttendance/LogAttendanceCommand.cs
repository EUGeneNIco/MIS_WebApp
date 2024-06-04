using MediatR;

namespace MIS.Application.GuestAttendanceLogs.Commands.LogGuestAttendance
{
    public class LogAttendanceCommand : IRequest<string>
    {
        public string Code { get; set; }
    }
}
