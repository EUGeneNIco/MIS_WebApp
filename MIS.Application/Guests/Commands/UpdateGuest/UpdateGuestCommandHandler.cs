using MediatR;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application._Interfaces;
using MIS.Domain.Entities;

namespace MIS.Application.Guests.Commands.UpdateGuest
{
    public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand, long>
    {
        private readonly IRepository<Guest> guestRepository;

        public UpdateGuestCommandHandler(IRepository<Guest> guestRepository)
        {
            this.guestRepository = guestRepository;
        }
        public async Task<long> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = await guestRepository.GetByIdAsync(request.Id);
            if (guest is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Guest"));

            var dbGuests = await guestRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(request.ContactNumber) && dbGuests.Any(x => x.ContactNumber == request.ContactNumber && x.Id != request.Id))
                throw new DuplicateException(ErrorMessages.DuplicateRecordError("contact number"));

            guest.ModifiedOn = DateTime.Now;

            guest.FirstName = request.FirstName.Trim();
            guest.LastName = request.LastName.Trim();
            guest.MiddleName = request.MiddleName.Trim();
            guest.BirthDate = request.BirthDate?.Date;
            guest.Address = request.Address;
            guest.Gender = request.Gender;
            guest.ContactNumber = request.ContactNumber;
            guest.CivilStatus = request.CivilStatus;
            guest.Age = request.Age != null && request.Age > 0 ? request.Age : null;
            guest.NetworkId = request.NetworkId;
            guest.Extension = request.Extension;

            guestRepository.Update(guest);
            await guestRepository.SaveChangesAsync(cancellationToken);

            return guest.Id;
        }
    }
}
