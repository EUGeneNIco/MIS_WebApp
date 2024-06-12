using AutoMapper;
using MediatR;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application._Interfaces;
using MIS.Application.Members.Models;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Queries.GetMember
{
    public class GetMemberQueryHandler : IRequestHandler<GetMemberQuery, MemberDto>
    {
        private readonly IRepository<Member> memberRepository;
        private readonly IMapper mapper;

        public GetMemberQueryHandler(IRepository<Member> memberRepository, IMapper mapper)
        {
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }
        public async Task<MemberDto> Handle(GetMemberQuery request, CancellationToken cancellationToken)
        {
            var member = await memberRepository.GetByIdAsync(request.Id);
            if (member is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Member"));

            return mapper.Map<MemberDto>(member);
        }
    }
}
