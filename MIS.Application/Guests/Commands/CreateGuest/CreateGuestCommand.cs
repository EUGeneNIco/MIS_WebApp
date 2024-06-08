using AutoMapper;
using MediatR;
using MIS.Application._Mappings;
using MIS.Domain.Entities;

namespace MIS.Application.Guests.Commands.CreateGuest
{
    public class CreateGuestCommand : IRequest<Unit>, IHaveCustomMapping
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string CivilStatus { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public string Extension { get; set; }

        public int? NetworkId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateGuestCommand, Guest>()
                .ForMember(x => x.FirstName, s => s.MapFrom(x => x.FirstName))
                .ForMember(x => x.MiddleName, s => s.MapFrom(x => x.MiddleName))
                .ForMember(x => x.LastName, s => s.MapFrom(x => x.LastName))
                .ForMember(x => x.Address, s => s.MapFrom(x => x.Address))
                .ForMember(x => x.Age, s => s.MapFrom(x => x.Age != null && x.Age > 0 ? x.Age : null))
                .ForMember(x => x.BirthDate, s => s.MapFrom(x => x.BirthDate));
        }
    }
}
