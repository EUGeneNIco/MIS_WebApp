using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application.Members.Models;
using MIS.Domain;

namespace MIS.Application.Members.Queries.GetMember
{
    public class GetMemberQueryHandler : IRequestHandler<GetMemberQuery, MemberDto>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public GetMemberQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<MemberDto> Handle(GetMemberQuery request, CancellationToken cancellationToken)
        {
            var member = await dbContext.Members
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (member is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Member"));

            return mapper.Map<MemberDto>(member);
        }
    }
}
