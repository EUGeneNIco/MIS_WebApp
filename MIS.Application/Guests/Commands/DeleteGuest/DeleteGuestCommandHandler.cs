using MediatR;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application._Interfaces.Guests;

namespace MIS.Application.Guests.Commands.DeleteGuest
{
    public class DeleteGuestCommandHandler : IRequestHandler<DeleteGuestCommand, Unit>
    {
        private readonly IGuestRepository guestRepository;
        private readonly IDeleteGuestActivity deleteGuestActivity;

        public DeleteGuestCommandHandler(IGuestRepository guestRepository, IDeleteGuestActivity deleteGuestActivity)
        {
            this.guestRepository = guestRepository;
            this.deleteGuestActivity = deleteGuestActivity;
        }

        public async Task<Unit> Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = await guestRepository.GetGuest(request.Id);
            if (guest is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Guest"));

            await deleteGuestActivity.Execute(guest, cancellationToken);

            return Unit.Value;
        }
    }
}
