using MediatR;
using MIS.Application.AttendanceLogs.Models;
using MIS.Application.Members.Models;

namespace MIS.Application.AttendanceLogs.Queries.GetMemberGuest
{
    public class GetMemberGuestQuery : IRequest<MemberGuestQueryDto>
    {
        public string Name { get; set; }
    }
}
