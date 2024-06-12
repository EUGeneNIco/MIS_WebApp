using AutoMapper;
using MediatR;
using MIS.Application._Mappings;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Commands.CreateMember
{
    public class CreateMemberCommand : IRequest<long>, IHaveCustomMapping
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string CivilStatus { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Category { get; set; }
        public int? Age { get; set; }
        public int? NetworkId { get; set; }
        public string Extension { get; set; }
        public string Status { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateMemberCommand, Member>()
                .ForMember(x => x.Age, s => s.MapFrom(x => x.Age != null && x.Age > 0 ? x.Age : null));
        }
    }
}
