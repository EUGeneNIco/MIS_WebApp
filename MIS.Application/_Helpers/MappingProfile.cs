using AutoMapper;
using MIS.Application.Members.Commands.CreateGuest;
using MIS.Application.Members.Commands.UpdateGuest;
using MIS.Domain.Entities;

namespace MIS.Application._Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<Guest, GuestDto>();
            CreateMap<Network, NetworkDto>();

            CreateMap<CreateGuestCommand, Guest>();
            CreateMap<UpdateGuestCommand, Guest>();
        }
    }
}
