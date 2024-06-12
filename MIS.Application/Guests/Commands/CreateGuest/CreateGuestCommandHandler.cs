using AutoMapper;
using MediatR;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application._Helpers;
using MIS.Application._Interfaces;
using MIS.Domain.Entities;

namespace MIS.Application.Guests.Commands.CreateGuest
{
    public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, Unit>
    {
        private readonly IRepository<Guest> guestRepository;
        private readonly IMapper mapper;

        public CreateGuestCommandHandler(IRepository<Guest> guestRepository,
                                         IMapper mapper)
        {
            this.guestRepository = guestRepository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = mapper.Map<Guest>(request);
            if (guest is null)
                throw new GenericException(ErrorMessages.GenericError);

            var dbGuests = await guestRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(request.ContactNumber) && dbGuests.Any(x => x.ContactNumber == request.ContactNumber))
                throw new DuplicateException(ErrorMessages.DuplicateRecordError("contact number"));

            var dateNow = DateTime.Now;
            guest.CreatedOn = dateNow;
            guest.Code = CodeHelper.GenerateGuestCode();

            await guestRepository.AddAsync(guest);
            await guestRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
