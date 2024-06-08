using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application._Interfaces.Guests;
using MIS.Domain;

namespace MIS.Application.Guests.Commands.UpdateGuest
{
    public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand, long>
    {
        private readonly IUpdateGuestActivity updateGuestActivity;
        private readonly IGuestRepository guestRepository;

        public UpdateGuestCommandHandler(IUpdateGuestActivity updateGuestActivity, IGuestRepository guestRepository)
        {
            this.updateGuestActivity = updateGuestActivity;
            this.guestRepository = guestRepository;
        }
        public async Task<long> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = await guestRepository.GetGuest(request.Id);
            if (guest is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Guest"));

            var dbGuests = await guestRepository.GetGuests();

            if (!string.IsNullOrWhiteSpace(request.ContactNumber) && dbGuests.Any(x => x.ContactNumber == request.ContactNumber && !x.IsDeleted && x.Id != request.Id))
                throw new DuplicateException(ErrorMessages.DuplicateRecordError("contact number"));

            await updateGuestActivity.Execute(guest, request, cancellationToken);

            return guest.Id;
        }
    }
}
