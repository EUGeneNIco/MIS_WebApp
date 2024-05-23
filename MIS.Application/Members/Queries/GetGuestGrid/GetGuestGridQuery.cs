using AFPMBAI.CLAIMS.Application.KIABenefits.Queries.GetKIABenefitsGrid;
using MediatR;
using MIS.Application._ViewModels;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Queries.GetGuestGrid
{
    public class GetGuestGridQuery : GridViewQuery, IRequest<GuestGridViewModel>
    {
    }
}
