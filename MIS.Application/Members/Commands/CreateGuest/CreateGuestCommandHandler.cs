using AutoMapper;
using MediatR;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Commands.CreateGuest
{
    public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, long>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public CreateGuestCommandHandler(IAppDbContext dbContext,
                                          IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<long> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = mapper.Map<Guest>(request);
            if (guest is null)
                throw new GenericException(ErrorMessages.GenericError);
            if (dbContext.Guests.Any(x => x.ContactNumber == request.ContactNumber && !x.IsDeleted))
                throw new DuplicateException(ErrorMessages.DuplicateRecordError("contact number"));

            guest.CreatedOn = DateTime.Now;

            dbContext.Guests.Add(guest);
            await dbContext.SaveChangesAsync(cancellationToken);

            return guest.Id;
        }
    }
}
