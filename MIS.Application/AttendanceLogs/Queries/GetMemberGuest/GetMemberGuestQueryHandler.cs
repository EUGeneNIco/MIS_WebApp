using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Interfaces;
using MIS.Application.AttendanceLogs.Models;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Application.AttendanceLogs.Queries.GetMemberGuest
{
    public class  GetMemberGuestQueryHandler : IRequestHandler< GetMemberGuestQuery, MemberGuestQueryDto>
    {
        private readonly IAppDbContext dbContext;
        private readonly IRepository<Guest> guestRepository;
        private readonly IRepository<Member> memberRepository;
        private readonly IMapper mapper;

        public  GetMemberGuestQueryHandler(IAppDbContext dbContext,
                                           IRepository<Guest> guestRepository,
                                           IRepository<Member> memberRepository,
                                           IMapper mapper)
        {
            this.dbContext = dbContext;
            this.guestRepository = guestRepository;
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }
        public async Task<MemberGuestQueryDto> Handle(GetMemberGuestQuery request, CancellationToken cancellationToken)
        {
            var requestName = request.Name.Trim().ToUpper();

            var dbMembers = await memberRepository.GetAllAsync();
            var query = dbMembers
                .Where(x => x.FirstName.ToUpper().Contains(requestName) ||
                            x.MiddleName.ToUpper().Contains(requestName) ||
                            x.LastName.ToUpper().Contains(requestName))
                .Select(x => new MemberGuestQueryItem
                {
                    FullName = $"{x.FirstName} {x.MiddleName} {x.LastName}",
                    Network = x.Network != null
                                ? x.Network.Name
                                : !string.IsNullOrWhiteSpace(x.NetworkImported) ? x.NetworkImported : string.Empty,
                    MemberId = x.Id
                }).ToList();

            var dbGuests = await guestRepository.GetAllAsync();

            var guestQuery = dbGuests
                .Where(x => x.FirstName.ToUpper().Contains(requestName) ||
                            x.MiddleName.ToUpper().Contains(requestName) ||
                            x.LastName.ToUpper().Contains(requestName))
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
