using AutoMapper;
using MediatR;
using MIS.Domain;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Members.Commands.CreateMember
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, int>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public CreateMemberCommandHandler(IAppDbContext dbContext,
                                          IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<int> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = this.mapper.Map<Member>(request);

            var totalCount = this.dbContext.Members.Count();

            member.MemberNumber = $"M-{DateTime.Now:yyyyMMdd}{totalCount}";

            this.dbContext.Members.Add(member);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return member.Id;
        }
    }
}
