using AutoMapper;
using MIS.Application._Mappings;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Models
{
    public class MemberDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string ContactNumber { get; set; }
        public string CivilStatus { get; set; }
        public string Extension { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public string NetworkImported { get; set; }
        public long? NetworkId { get; set; }
        public DateTime? ImportDate { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Member, MemberDto>();
        }
    }
}
