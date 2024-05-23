using AFPMBAI.CLAIMS.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Commands.DeleteGuest
{
    public class DeleteGuestCommandHandler : IRequestHandler<DeleteGuestCommand, Unit>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public DeleteGuestCommandHandler(IAppDbContext dbContext,
                                          IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = await dbContext.Guests
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (guest is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Guest"));

            guest.IsDeleted = true;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
