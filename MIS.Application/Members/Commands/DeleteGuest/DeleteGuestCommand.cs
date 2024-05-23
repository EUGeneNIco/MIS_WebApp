using MediatR;

namespace MIS.Application.Members.Commands.DeleteGuest
{
    public class DeleteGuestCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
