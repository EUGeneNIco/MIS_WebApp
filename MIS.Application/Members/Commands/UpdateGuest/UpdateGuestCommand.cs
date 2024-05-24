using AutoMapper;
using MediatR;
using MIS.Application._Mappings;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Commands.UpdateGuest
{
    public class UpdateGuestCommand : IRequest<long>, IHaveCustomMapping
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string CivilStatus { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? Age { get; set; }

        public int? NetworkId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateGuestCommand, Guest>();
        }
    }
}
