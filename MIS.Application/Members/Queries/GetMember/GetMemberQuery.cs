using MediatR;
using MIS.Application.Members.Models;

namespace MIS.Application.Members.Queries.GetMember
{
    public class GetMemberQuery : IRequest<MemberDto>
    {
        public long Id { get; set; }
    }
}
