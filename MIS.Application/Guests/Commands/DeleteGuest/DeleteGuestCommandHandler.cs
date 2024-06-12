using MediatR;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application._Interfaces;
using MIS.Domain.Entities;

namespace MIS.Application.Guests.Commands.DeleteGuest
{
    public class DeleteGuestCommandHandler : IRequestHandler<DeleteGuestCommand, Unit>
    {
        private readonly IRepository<Guest> guestRepository;

        public DeleteGuestCommandHandler(IRepository<Guest> guestRepository)
        {
            this.guestRepository = guestRepository;
        }

        public async Task<Unit> Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = await guestRepository.GetByIdAsync(request.Id);
            if (guest is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Guest"));

            guestRepository.Delete(guest);
            await guestRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
