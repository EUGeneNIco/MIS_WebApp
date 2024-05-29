using AutoMapper;
using MediatR;
using MIS.Application._Mappings;
using MIS.Application.Guests.Commands.CreateGuest;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Commands.ImportMemberData
{
    public class ImportMemberDataCommand : IRequest<Unit>
    {
        public List<MemberData> ImportedData { get; set; }
    }

    public class MemberData
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? Age { get; set; }
        public DateTime? BirthDate { get; set; } // mapp
        public string Category { get; set; }
        public string MemberCode { get; set; } // map
        public string Extension { get; set; }
        public string NetworkImported { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string CivilStatus { get; set; }

        //public void CreateMappings(Profile configuration)
        //{
        //    configuration.CreateMap<MemberData, Member>()
        //        .ForMember(x => x.Birthdate, s => s.MapFrom(x => x.BirthDay))
        //        .ForMember(x => x.NetworkImported, s => s.MapFrom(x => x.Network))
        //        .ForMember(x => x.MemberCode, s => s.MapFrom(x => x.Code));
        //}
    }
}
