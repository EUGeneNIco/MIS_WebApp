using MediatR;

namespace MIS.Application.Guests.Commands.DeleteGuest
{
    public class DeleteGuestCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
