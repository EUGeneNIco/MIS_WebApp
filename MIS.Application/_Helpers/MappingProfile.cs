using AutoMapper;
using MIS.Application.Members.Commands.CreateMember;
using MIS.Domain.Entities;

namespace MIS.Application._Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<CreateMemberCommand, Member>();
            CreateMap<Network, NetworkDto>();
        }
    }
}
