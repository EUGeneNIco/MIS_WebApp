using AutoMapper;
using MediatR;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application._Helpers;
using MIS.Application._Interfaces.Guests;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Application.Guests.Commands.CreateGuest
{
    public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, Unit>
    {
        private readonly IAddGuestActivity guestActivity;
        private readonly IGuestRepository guestRepository;
        private readonly IMapper mapper;

        public CreateGuestCommandHandler(IAddGuestActivity guestActivity, IGuestRepository guestRepository,
                                         IMapper mapper)
        {
            this.guestActivity = guestActivity;
            this.guestRepository = guestRepository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = mapper.Map<Guest>(request);
            if (guest is null)
                throw new GenericException(ErrorMessages.GenericError);

            var dbGuests = await guestRepository.GetGuests();

            if (!string.IsNullOrWhiteSpace(request.ContactNumber) && dbGuests.Any(x => x.ContactNumber == request.ContactNumber && !x.IsDeleted))
                throw new DuplicateException(ErrorMessages.DuplicateRecordError("contact number"));

            await guestActivity.Execute(guest, cancellationToken);

            return Unit.Value;
        }
    }
}
