using MediatR;
using MIS.Application._ViewModels;
using MIS.Domain.Entities;

namespace MIS.Application.Guests.Queries.GetGuestGrid
{
    public class GetGuestGridQuery : GridViewQuery, IRequest<GuestGridViewModel>
    {
    }
}
