using MediatR;

namespace MIS.Application.AttendanceLogs.Commands.LogGuestAttendance
{
    public class LogAttendanceCommand : IRequest<string>
    {
        public string Code { get; set; }
        public long? MemberId { get; set; }
        public long? GuestId { get; set; }
    }
}
