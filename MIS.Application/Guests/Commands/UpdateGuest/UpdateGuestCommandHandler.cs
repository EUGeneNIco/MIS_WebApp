using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Domain;

namespace MIS.Application.Guests.Commands.UpdateGuest
{
    public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand, long>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public UpdateGuestCommandHandler(IAppDbContext dbContext,
                                          IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<long> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = await dbContext.Guests.FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (guest is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Guest"));

            if (!string.IsNullOrWhiteSpace(request.ContactNumber) && dbContext.Guests.Any(x => x.ContactNumber == request.ContactNumber && !x.IsDeleted && x.Id != request.Id))
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

            await dbContext.SaveChangesAsync(cancellationToken);

            return guest.Id;
        }
    }
}
