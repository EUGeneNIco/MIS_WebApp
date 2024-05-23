using AFPMBAI.CLAIMS.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Domain;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Members.Queries.GetGuest
{
    public class GetGuestQueryHandler : IRequestHandler<GetGuestQuery, GuestDto>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public GetGuestQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<GuestDto> Handle(GetGuestQuery request, CancellationToken cancellationToken)
        {
            var guest = await dbContext.Guests
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (guest is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Guest"));

            return this.mapper.Map<GuestDto>(guest);
        }
    }
}
