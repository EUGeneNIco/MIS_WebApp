using MediatR;
using MIS.Application._ViewModels;

namespace MIS.Application.Members.Queries.GetMemberGrid
{
    public class GetMemberGridQuery : GridViewQuery, IRequest<MemberGridViewModel>
    {
    }
}
