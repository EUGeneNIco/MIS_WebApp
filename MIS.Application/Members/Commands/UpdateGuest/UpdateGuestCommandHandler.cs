using AFPMBAI.CLAIMS.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Commands.UpdateGuest
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
            var guest = await this.dbContext.Guests.FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (guest is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Guest"));

            guest.ModifiedOn = DateTime.Now;

            guest.FirstName = request.FirstName;
            guest.LastName = request.LastName;
            guest.MiddleName = request.MiddleName;
            guest.Birthdate = request.Birthdate;
            guest.Address = request.Address;
            guest.Gender = request.Gender;
            guest.ContactNumber = request.ContactNumber;
            guest.CivilStatus = request.CivilStatus;
            guest.Birthdate = request.Birthdate;
            guest.Age = request.Age;

            await dbContext.SaveChangesAsync(cancellationToken);

            return guest.Id;
        }
    }
}
