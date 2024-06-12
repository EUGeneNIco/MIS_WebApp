using AutoMapper;
using MediatR;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application._Helpers;
using MIS.Application._Interfaces;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Commands.CreateMember
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, long>
    {
        private readonly IRepository<Member> memberRepository;
        private readonly IMapper mapper;

        public CreateMemberCommandHandler(IRepository<Member> memberRepository, IMapper mapper)
        {
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }
        public async Task<long> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = mapper.Map<Member>(request);
            if (member is null)
                throw new GenericException(ErrorMessages.GenericError);

            var dbMembers = await memberRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(request.ContactNumber) && dbMembers.Any(x => x.ContactNumber == request.ContactNumber))
                throw new DuplicateException(ErrorMessages.DuplicateRecordError("contact number"));

            var dateNow = DateTime.Now;
            member.CreatedOn = dateNow;
            member.MemberCode = CodeHelper.GenerateGuestCode();
            member.NetworkImported = string.Empty; // not imported

            await memberRepository.AddAsync(member);
            await memberRepository.SaveChangesAsync(cancellationToken);

            return member.Id;
        }
    }
}
