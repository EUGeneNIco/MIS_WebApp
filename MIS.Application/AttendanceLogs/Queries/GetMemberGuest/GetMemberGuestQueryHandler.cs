using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application.AttendanceLogs.Models;
using MIS.Application.Members.Models;
using MIS.Application.Members.Queries.GetMember;
using MIS.Domain;

namespace MIS.Application.AttendanceLogs.Queries.GetMemberGuest
{
    public class  GetMemberGuestQueryHandler : IRequestHandler< GetMemberGuestQuery, MemberGuestQueryDto>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public  GetMemberGuestQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<MemberGuestQueryDto> Handle(GetMemberGuestQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Members
                .Include(x => x.Network)
                .Where(x => x.FirstName.Contains(request.Name) ||
                            x.MiddleName.Contains(request.Name) ||
                            x.LastName.Contains(request.Name) && !x.IsDeleted)
                .Select(x => new MemberGuestQueryItem
                {
                    FullName = $"{x.FirstName} {x.MiddleName} {x.LastName}",
                    Network = x.Network != null
                                ? x.Network.Name
                                : !string.IsNullOrWhiteSpace(x.NetworkImported) ? x.NetworkImported : string.Empty,
                    MemberId = x.Id
                }).ToList();

            var guestQuery = dbContext.Guests
                .Include(x => x.Network)
                .Where(x => x.FirstName.Contains(request.Name) ||
                            x.MiddleName.Contains(request.Name) ||
                            x.LastName.Contains(request.Name) && !x.IsDeleted)
                .Select(x => new MemberGuestQueryItem
                {
                    FullName = $"{x.FirstName} {x.MiddleName} {x.LastName}",
                    Network = x.Network != null ? x.Network.Name : "",
                    GuestId = x.Id
                }).ToList();

            query.AddRange(guestQuery);

            return new MemberGuestQueryDto
            {
                Total = query.Count(),
                Results = query,
            };
        }
    }
}
