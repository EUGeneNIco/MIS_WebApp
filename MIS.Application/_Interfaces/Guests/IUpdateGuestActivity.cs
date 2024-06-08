using MIS.Application.Guests.Commands.UpdateGuest;
using MIS.Domain.Entities;

namespace MIS.Application._Interfaces.Guests
{
    public interface IUpdateGuestActivity
    {
        Task Execute(Guest guest, UpdateGuestCommand request, CancellationToken cancellationToken);
    }
}
