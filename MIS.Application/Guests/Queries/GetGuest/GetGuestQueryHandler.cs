using AutoMapper;
using MediatR;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application._Interfaces.Guests;
using MIS.Application.Guests.Models;

namespace MIS.Application.Guests.Queries.GetGuest
{
    public class GetGuestQueryHandler : IRequestHandler<GetGuestQuery, GuestDto>
    {
        private readonly IGuestRepository guestRepository;
        private readonly IMapper mapper;

        public GetGuestQueryHandler(IGuestRepository guestRepository, IMapper mapper)
        {
            this.guestRepository = guestRepository;
            this.mapper = mapper;
        }
        public async Task<GuestDto> Handle(GetGuestQuery request, CancellationToken cancellationToken)
        {
            var guest = await guestRepository.GetGuest(request.Id);
            if (guest is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Guest"));

            return mapper.Map<GuestDto>(guest);
        }
    }
}
