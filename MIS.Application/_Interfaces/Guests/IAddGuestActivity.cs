using MIS.Domain.Entities;

namespace MIS.Application._Interfaces.Guests
{
    public interface IAddGuestActivity
    {
        Task Execute(Guest guest, CancellationToken cancellationToken);
    }
}
